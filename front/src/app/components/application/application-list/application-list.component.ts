import { AfterViewInit, Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { ApplicationReadDto } from '../../../../dtos/application-read.dto';
import { ApplicationService } from '../../../../services/application.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { ApplicationCreationComponent } from '../application-creation/application-creation.component';


@Component({
  selector: 'app-application-list',
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
  templateUrl: './application-list.component.html',
  styleUrl: './application-list.component.css'
})
export class ApplicationListComponent implements AfterViewInit, OnInit{

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
  displayedColumns: string[] = ['application_name', 'application_type'];

  /**
   * Data source for the Material Table.
   */
  dataSource = new MatTableDataSource<ApplicationReadDto>([]);

  /**
   * Flag to indicate whether the application list is currently being fetched.
   */
  isLoading = false;

  /**
   * Initializes the `ApplicationListComponent` and fetches the list of applications.
   * 
   * @param dialog - Injected `MatDialog` service for opening modal dialogs, such as the `ApplicationCreateComponent`.
   * @param applicationService - Injected `ApplicationService` for handling API requests related to applications.
   * @param snackBar - Injected `MatSnackBar` service for displaying notifications to the user.
   */
  constructor(private dialog: MatDialog, private applicationService: ApplicationService, private snackBar: MatSnackBar) {
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
    this.fetchApplications();
  }

  /**
   * Fetches the list of applications from the server.
   * Displays a loading spinner during the fetch and handles errors by showing a notification.
   */
  private fetchApplications(): void {
    this.isLoading = true;
    this.applicationService.getAll().subscribe({
      next: (data) => {
        console.log('Fetched applications:', data);
        this.dataSource = new MatTableDataSource<ApplicationReadDto>(
          data.map(app => ({
            application_id: app.application_id,
            application_name: app.application_name,
            application_type: app.application_type
          }))
        );
        
        this.dataSource.paginator = this.paginator;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error fetching applications:', error);
        this.openSnackBar('Failed to fetch applications. Please try again.', 'Close');
        this.isLoading = false;
      }
    });
  }

  /**
   * Opens a dialog for creating a new application.
   * After the dialog is closed, the application list is refreshed if a new application was created.
   */
  openCreateApplication(): void {
    const dialogRef = this.dialog.open(ApplicationCreationComponent, {
      width: '50vw',
      height: '60vh',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result === true) {
        this.fetchApplications();
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
