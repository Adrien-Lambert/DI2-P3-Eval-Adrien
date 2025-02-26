import { Component, OnInit } from '@angular/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ApplicationService } from '../../../../services/application.service';
import { MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { PasswordCreationDto } from '../../../../dtos/password-creation.dto';
import { PasswordService } from '../../../../services/password.service';
import { ApplicationReadDto } from '../../../../dtos/application-read.dto';

@Component({
  selector: 'app-password-creation',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDialogContent,
    MatSnackBarModule,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './password-creation.component.html',
  styleUrl: './password-creation.component.css'
})
export class PasswordCreationComponent implements OnInit{

  /**
   * Data model for the password being created.
   * Pre-filled with default values.
   */
  public passwordData: PasswordCreationDto = {
    account_name: '',
    password: '',
    application_id: 0
  };

  /** Boolean flag to toggle password visibility in the form. */
  passwordVisible = false;

  /** List of available applications to show in the combobox. */
  applications: ApplicationReadDto[] = [];

  /**
   * Constructor injecting necessary services.
   * @param passwordService Service for creating and managing passwords
   * @param applicationService Service for creating and managing applications
   * @param snackBar Service for displaying snack-bar notifications
   * @param dialogRef Reference to the dialog containing this component
   */
  constructor(
    private passwordService: PasswordService,
    private applicationService: ApplicationService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<PasswordCreationComponent>
  ) {}

  ngOnInit(): void {
    this.fetchApplications();
  }

  /**
   * Handler for the form submission event.
   * Sends the password data to the backend and displays feedback to the user.
   */
  onSubmit(): void {
    console.log('Form submitted:', this.passwordData);
    this.passwordService.createPassword(this.passwordData).subscribe({
      next: (response) => {
        console.log('Password created:', response);
        this.openSnackBar('Password created successfully!', 'Close');
        this.closeDialog(true);
      },
      error: (error) => {
        console.error('Error creating password:', error);
        if (error.status === 409) {
          this.openSnackBar('A password with this account name already exists. Please use a different name.', 'Close');
        } else {
          this.openSnackBar('Failed to create password. Please try again.', 'Close');
        }
      }
    });
  }

  /**
   * Fetches the list of applications from the server.
   * Displays a loading spinner during the fetch and handles errors by showing a notification.
   */
  private fetchApplications(): void {
    this.applicationService.getAll().subscribe({
      next: (data) => {
        console.log('Fetched applications:', data);
        this.applications = data.map(app => ({
          application_id: app.application_id,
          application_name: app.application_name,
          application_type: app.application_type
        })) as ApplicationReadDto[];
      },
      error: (error) => {
        console.error('Error fetching applications:', error);
        this.openSnackBar('Failed to fetch applications. Please try again.', 'Close');
      }
    });
  }

  /**
   * Toggles the visibility of the password in the form.
   * Used for user convenience during password input.
   */
  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible;
  }

  /**
   * Displays a snack-bar notification to the user.
   * @param message The message to display
   * @param action The label for the action button (e.g., "Close")
   */
  private openSnackBar(message : string, action : string){
    console.log('SnackBar called with:', message, action);
    this.snackBar.open(message, action, {
      duration: 5000,
      panelClass: ['error-snackbar']
    });  
  }

  /**
   * Closes the dialog and passes a boolean indicating success or failure.
   * @param created `true` if the application was successfully created, otherwise `false`
   */
  closeDialog(created: boolean): void {
    this.dialogRef.close(created);
  }

}
