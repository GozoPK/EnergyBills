import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Admin } from '../models/admin';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  private errorMessagesSubject = new BehaviorSubject<string[] | null>(null);
  errorMessages$ = this.errorMessagesSubject.asObservable();

  constructor(private http: HttpClient) { }

  createUser(admin: Admin) {
    return this.http.post(`${this.baseUrl}/admin`, admin).pipe(
      catchError(error => this.handleError(error))
    );
  }

  setErrorMessages(errors: string[] | null) {
    this.errorMessagesSubject.next(errors);
  }

  handleError(error: HttpErrorResponse) {
    if (error.status == 400 || error.status == 401) {
      if (error.error.failedToAuthenticate) {
        const errorMessage = [error.error.message];
        console.log(errorMessage);
        this.setErrorMessages(errorMessage);
        return EMPTY;
      }
      const errorMessages = error.error.errors;
      this.setErrorMessages(errorMessages);
      return EMPTY;
    }
    return EMPTY;
  }
}
