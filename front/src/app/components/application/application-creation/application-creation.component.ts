import { Component } from '@angular/core';
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
import { ApplicationCreationDto } from '../../../../dtos/application-creation.dto';
import { ApplicationType } from '../../../../enums/application-type.enum';

@Component({
  selector: 'app-application-creation',
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
  templateUrl: './application-creation.component.html',
  styleUrl: './application-creation.component.css'
})
export class ApplicationCreationComponent {

    /**
   * Data model for the application being created.
   * Pre-filled with default values.
   */
    public applicationData: ApplicationCreationDto = {
      application_name: '',
      application_type: Object.keys(ApplicationType).indexOf('PUBLIC')
    };

    applicationTypes = Object.values(ApplicationType);

  /**
   * Constructor injecting necessary services.
   * @param applicationService Service for creating and managing applications
   * @param snackBar Service for displaying snack-bar notifications
   * @param dialogRef Reference to the dialog containing this component
   */
  constructor(
    private applicationService: ApplicationService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ApplicationCreationComponent>
  ) {}

  /**
   * Handler for the form submission event.
   * Sends the application data to the backend and displays feedback to the user.
   */
  onSubmit(): void {
    console.log('Form submitted:', this.applicationData);
    this.applicationService.createApplication(this.applicationData).subscribe({
      next: (response) => {
        console.log('Application created:', response);
        this.openSnackBar('Application created successfully!', 'Close');
        this.closeDialog(true);
      },
      error: (error) => {
        console.error('Error creating application:', error);
        if (error.status === 409) {
          this.openSnackBar('An application with this name already exists. Please use a different name.', 'Close');
        } else {
          this.openSnackBar('Failed to create application. Please try again.', 'Close');
        }
      }
    });
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
