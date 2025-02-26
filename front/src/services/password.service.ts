import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Password } from '../models/password.model';
import { environment } from '../environments/environment';
import { PasswordCreationDto } from '../dtos/password-creation.dto';
import { PasswordReadDto } from '../dtos/password-read.dto';

/**
 * Service for handling HTTP operations related to `Password` entities.
 * Provides methods for creating, retrieving, or deleting passwords.
 */
@Injectable({
  providedIn: 'root'
})
export class PasswordService {

  /**
   * Base URL for the API endpoint, configured from environment variables.
   */
  private readonly baseUrl = environment.apiUrl + '/passwords';

  /**
   * Constructor for `PasswordService`.
   * @param http - Instance of `HttpClient` for making HTTP requests.
   */
  constructor(private http: HttpClient) {}

  /**
   * Sends a POST request to create a new password.
   * @param appDto - `PasswordCreationDto` containing the details of the password to be created.
   * @returns `Observable<Password>` - The created `Password` object.
   */
  createPassword(appDto: PasswordCreationDto): Observable<Password> {
    return this.http.post<Password>(this.baseUrl, appDto);
  }

  /**
   * Sends a GET request to retrieve all passwords.
   * @returns `Observable<PasswordReadDto[]>` - A list of all passwords as `PasswordReadDto` objects.
   */
  getAll(): Observable<PasswordReadDto[]> {
    return this.http.get<PasswordReadDto[]>(this.baseUrl);
  }

  /**
   * Sends a DELETE request to remove an password by its ID.
   * @param passwordId - The ID of the password to delete.
   * @returns `Observable<void>` - An observable that completes when the deletion is successful.
   */
  deletePassword(passwordId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${passwordId}`);
  }  
}
