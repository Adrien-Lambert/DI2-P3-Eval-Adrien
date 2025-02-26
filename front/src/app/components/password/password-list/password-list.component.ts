import { AfterViewInit, Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { PasswordReadDto } from '../../../../dtos/password-read.dto';
import { PasswordService } from '../../../../services/password.service';
import { PasswordCreationComponent } from '../password-creation/password-creation.component';

@Component({
  selector: 'app-password-list',
  standalone: true,
  imports: [
    MatIconModule,
    MatListModule,
    MatDialogModule,
    MatTableModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatSortModule
  ],
  templateUrl: './password-list.component.html',
  styleUrl: './password-list.component.css'
})
export class PasswordListComponent implements AfterViewInit, OnInit{

  /**
   * Reference to the Material Paginator to enable table pagination.
   */
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  /**
   * Reference to the Material Sort to enable table sorting.
   */
  @ViewChild(MatSort) set matSort(sort: MatSort) {
    if (!this.dataSource.sort) {
        this.dataSource.sort = sort;
    }
  }

  /**
   * Columns displayed in the Material Table.
   */
  displayedColumns: string[] = ['application_name', 'account_name', 'password', 'actions'];

   /**
   * Data source for the Material Table.
   */
  dataSource = new MatTableDataSource<PasswordReadDto>([]);

  /**
   * Flag to indicate whether the password list is currently being fetched.
   */
  isLoading = false;

  /**
   * Initializes the `PasswordListComponent` and fetches the list of passwords.
   * 
   * @param dialog - Injected `MatDialog` service for opening modal dialogs, such as the `PasswordCreateComponent`.
   * @param passwordService - Injected `PasswordService` for handling API requests related to passwords.
   * @param snackBar - Injected `MatSnackBar` service for displaying notifications to the user.
   */
  constructor(private dialog: MatDialog, private passwordService: PasswordService, private snackBar: MatSnackBar) {
    this.isLoading = false;
  }

  /**
   * Lifecycle hook called after Angular has fully initialized the component's view.
   * Links the paginator and sort components to the table data source.
   */
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }

  ngOnInit(): void {
    this.fetchPasswords();
  }

  /**
   * Fetches the list of passwords from the server.
   * Displays a loading spinner during the fetch and handles errors by showing a notification.
   */
  private fetchPasswords(): void {
    this.isLoading = true;
    this.passwordService.getAll().subscribe({
      next: (data) => {
        console.log('Fetched passwords:', data);
        this.dataSource = new MatTableDataSource<PasswordReadDto>(
          data.map(pass => ({
            password_id: pass.password_id,
            account_name: pass.account_name,
            password: pass.password,
            application: pass.application
            
          }))
        );
        
        this.dataSource.paginator = this.paginator;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error fetching applications:', error);
        this.openSnackBar('Failed to fetch passwords. Please try again.', 'Close');
        this.isLoading = false;
      }
    });
  }

  /**
   * Deletes the specified password after user confirmation.
   * Refreshes the password list upon successful deletion.
   * @param password The password to be deleted.
   */
  onDelete(password: PasswordReadDto): void {
    this.passwordService.deletePassword(password.password_id).subscribe({
      next: () => {
        console.log('Password deleted successfully');
        this.openSnackBar('Password deleted successfully.', 'Close');
        this.fetchPasswords();
      },
      error: (error) => {
        console.error('Error deleting password:', error);
        if (error.status === 404) {
          this.openSnackBar('Did not find any password with the selected id.', 'Close');
        } else {
          this.openSnackBar('Failed to delete password. Please try again.', 'Close');
        }
        this.fetchPasswords();
      },
    });
  }

    /**
   * Opens a dialog for creating a new password.
   * After the dialog is closed, the password list is refreshed if a new password was created.
   */
    openCreatePassword(): void {
      const dialogRef = this.dialog.open(PasswordCreationComponent, {
        width: '50vw',
        height: '60vh',
        data: {}
      });
  
      dialogRef.afterClosed().subscribe((result) => {
        if (result === true) {
          this.fetchPasswords();
        }
      });
    }

  /**
   * Displays a notification with the given message and action.
   * @param message The message to display in the notification.
   * @param action The action label for the notification (e.g., "Close").
   */
  private openSnackBar(message : string, action : string){
    this.snackBar.open(message, action, {
      duration: 5000,
      panelClass: ['error-snackbar']
    });  
  }
}
