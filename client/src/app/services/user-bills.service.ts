import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
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
      })
    );
  }

  setUserParams(userParams: UserParams) {
    this.userParamsSubject.next(userParams);
  }
}
