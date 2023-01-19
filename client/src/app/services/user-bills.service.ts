import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateBill } from '../models/createBill';
import { PagedList } from '../models/pagingData';
import { UserBill } from '../models/userBill';
import { UserParams } from '../models/userParams';

@Injectable({
  providedIn: 'root'
})
export class UserBillsService {
  baseUrl = environment.apiUrl;

  userParamsSubject = new BehaviorSubject<UserParams>(new UserParams);
  userParams$ = this.userParamsSubject.asObservable();

  private errorMessagesSubject = new BehaviorSubject<string[] | null>(null);
  errorMessages$ = this.errorMessagesSubject.asObservable();

  constructor(private http: HttpClient) { }

  getUserBills(userParams: UserParams) {
    const pagedList = new PagedList<UserBill[]>();
    let params = new HttpParams();

    params = params.append('pageNumber', userParams.pageNumber);
    params = params.append('pageSize', userParams.pageSize);
    params = params.append('type', userParams.type);
    params = params.append('status', userParams.status);
    params = params.append('orderby', userParams.orderBy);
    params = params.append('minMonth', userParams.minMonth);
    params = params.append('minYear', userParams.minYear);
    params = params.append('maxMonth', userParams.maxMonth);
    params = params.append('maxYear', userParams.maxYear);

    return this.http.get<UserBill[]>(`${this.baseUrl}/user/bills`, { observe: 'response', params}).pipe(
      map(response => {
        if (response.body) pagedList.result = response.body;
        const pagingHeaders = response.headers.get('pagination');
        if (pagingHeaders) pagedList.pagination = JSON.parse(pagingHeaders);
        return pagedList;
      }),
      catchError(error => this.handleError(error))
    );
  }

  createBill(billForm: CreateBill) {
    return this.http.post(`${this.baseUrl}/user/bills`, billForm).pipe(
      catchError(error => this.handleError(error))
    );
  }

  setUserParams(userParams: UserParams) {
    this.userParamsSubject.next(userParams);
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
