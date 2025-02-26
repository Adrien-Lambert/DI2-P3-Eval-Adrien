import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Application } from '../models/application.model';
import { environment } from '../environments/environment';
import { ApplicationCreationDto } from '../dtos/application-creation.dto';
import { ApplicationReadDto } from '../dtos/application-read.dto';

/**
 * Service for handling HTTP operations related to `Application` entities.
 * Provides methods for creating, retrieving, or deleting applications.
 */
@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  /**
   * Base URL for the API endpoint, configured from environment variables.
   */
  private readonly baseUrl = environment.apiUrl + '/applications';

  /**
   * Constructor for `ApplicationService`.
   * @param http - Instance of `HttpClient` for making HTTP requests.
   */
  constructor(private http: HttpClient) {}

  /**
   * Sends a POST request to create a new application.
   * @param appDto - `ApplicationCreationDto` containing the details of the application to be created.
   * @returns `Observable<Application>` - The created `Application` object.
   */
  createApplication(appDto: ApplicationCreationDto): Observable<Application> {
    return this.http.post<Application>(this.baseUrl, appDto);
  }

  /**
   * Sends a GET request to retrieve all applications.
   * @returns `Observable<ApplicationReadDto[]>` - A list of all applications as `ApplicationReadDto` objects.
   */
  getAll(): Observable<ApplicationReadDto[]> {
    return this.http.get<ApplicationReadDto[]>(this.baseUrl);
  }
}
