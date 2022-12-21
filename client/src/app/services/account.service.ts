import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from '../models/login';
import { environment } from 'src/environments/environment';
import { TaxisnetUser } from '../models/TaxisnetUser';
import {map} from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = environment.apiUrl;
  user: TaxisnetUser | undefined;
  
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
      })
    );
  }

  login(user: Login) {
    return this.http.post<User>(`${this.baseUrl}/account/login`, user).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user, true);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(`${this.baseUrl}/account/register`, model).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user, true);
        }
      })
    )
  }

  setCurrentUser(user: TaxisnetUser | User, isRegistered: boolean) {
    const currentUser: User = {
      username: user.username,
      token: user.token,
      afm: user.afm,
      annualIncome: user.annualIncome,
      isRegistered: isRegistered
    };

    localStorage.setItem('user', JSON.stringify(currentUser));

    this.currentUserSubject.next(currentUser);
  }

  logout() {
    this.currentUserSubject.next(null);
    localStorage.removeItem('user');
  }

}
