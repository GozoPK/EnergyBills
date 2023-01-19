import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Login } from '../models/login';
import { environment } from 'src/environments/environment';
import { TaxisnetUser } from '../models/TaxisnetUser';
import {catchError, map} from 'rxjs/operators';
import { BehaviorSubject, EMPTY } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = environment.apiUrl;
  user: TaxisnetUser | undefined;

  private errorMessagesSubject = new BehaviorSubject<string[] | null>(null);
  errorMessages$ = this.errorMessagesSubject.asObservable();
  
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSubject.asObservable()

  constructor(private http: HttpClient) { }

  getCurrentUser() {
    return this.http.get<User>(`${this.baseUrl}/account`).pipe(
      map(user => {
        this.setCurrentUser(user);
        return user;         
      })
    );
  }

  taxisnetLogin(user: Login) {
    return this.http.post<TaxisnetUser>(`${this.baseUrl}/account/taxisnet-login`, user).pipe(
      map((user: TaxisnetUser) => {
        if (user) {
          this.user = user;
          localStorage.setItem('token', user.token)
        }
      }),
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  login(user: Login) {
    return this.http.post<User>(`${this.baseUrl}/account/login`, user).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token)
          this.setCurrentUser(user);
        }
      }),
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  register(model: any) {
    return this.http.post<User>(`${this.baseUrl}/account/register`, model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token)
          this.user = undefined;
          this.setCurrentUser(user);
        }
      }),
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  setCurrentUser(user: TaxisnetUser | User | null) {
    if (user) {
      const currentUser: User = {
        username: user.username,
        afm: user.afm,
        token: user.token,
        annualIncome: user.annualIncome
      };
  
      this.currentUserSubject.next(currentUser);
      return;
    }
    
    this.currentUserSubject.next(null);
  }

  setErrorMessages(errors: string[] | null) {
    this.errorMessagesSubject.next(errors);
  }

  logout() {
    this.currentUserSubject.next(null);
    localStorage.removeItem('token');
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
