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

  private errorMessageSubject = new BehaviorSubject<string | null>(null);
  errorMessage$ = this.errorMessageSubject.asObservable();
  
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSubject.asObservable()

  constructor(private http: HttpClient) { }

  taxisnetLogin(user: Login) {
    return this.http.post<TaxisnetUser>(`${this.baseUrl}/account/taxisnet-login`, user).pipe(
      map((user: TaxisnetUser) => {
        if (user) {
          this.user = user;
          this.setCurrentUser(user, false);
        }
      }),
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  login(user: Login) {
    return this.http.post<User>(`${this.baseUrl}/account/login`, user).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user, true);
        }
      }),
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  register(model: any) {
    return this.http.post<User>(`${this.baseUrl}/account/register`, model).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user, true);
        }
      }),
      catchError((error: HttpErrorResponse) => this.handleError(error))
    );
  }

  setCurrentUser(user: TaxisnetUser | User | null, isRegistered: boolean) {
    if (user) {
      const currentUser: User = {
        username: user.username,
        token: user.token,
        afm: user.afm,
        annualIncome: user.annualIncome,
        isRegistered: isRegistered
      };
  
      localStorage.setItem('user', JSON.stringify(currentUser));
  
      this.currentUserSubject.next(currentUser);
      return;
    }
    
    this.currentUserSubject.next(null);
  }

  setErrorMessage(error: string | null) {
    this.errorMessageSubject.next(error);
  }

  logout() {
    this.currentUserSubject.next(null);
    localStorage.removeItem('user');
  }

  handleError(error: HttpErrorResponse) {
    if (error.status == 400 || error.status == 401) {
      this.setErrorMessage(error.error);
      return EMPTY;
    }
    return EMPTY;
  }
  
}
