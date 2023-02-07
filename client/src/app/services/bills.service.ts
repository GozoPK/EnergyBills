import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PagedList } from '../models/pagingData';
import { UserBill } from '../models/userBill';
import { UserParams } from '../models/userParams';
import { ModalService } from './modal.service';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  baseUrl = environment.apiUrl;

  private pagedListSubject = new BehaviorSubject<PagedList<UserBill[]>>(new PagedList<UserBill[]>());
  pagedList$ = this.pagedListSubject.asObservable();

  constructor(private http: HttpClient, private modalService: ModalService) { }

  getBills(userParams: UserParams) {
    const pagedList = new PagedList<UserBill[]>();
    let params = new HttpParams();

    params = params.append('pageNumber', userParams.pageNumber);
    params = params.append('pageSize', userParams.pageSize);

    return this.http.get<UserBill[]>(`${this.baseUrl}/bills`, { observe: 'response', params}).pipe(
      map(response => {
        if (response.body) pagedList.result = response.body;
        const pagingHeaders = response.headers.get('pagination');
        if (pagingHeaders) pagedList.pagination = JSON.parse(pagingHeaders);
        return pagedList;
      }),
      catchError(error => this.handleError(error))
    );
  }

  getBill(id: string) {
    return this.http.get<UserBill>(`${this.baseUrl}/bills/${id}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  updateBillStatus(id: string, status: number) {
    return this.http.put(`${this.baseUrl}/bills/${id}`, { status }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  setPagedList(list: PagedList<UserBill[]>) {
    this.pagedListSubject.next(list);
  }

  handleError(error: HttpErrorResponse) {
    if (error.status == 400 || error.status == 401) {
      if (error.error.failedToAuthenticate) {
        const errorMessage = [error.error.message];
        this.modalService.operModal(errorMessage.join('\r\n'));
        return EMPTY;
      }
      const errorMessages = error.error.errors;
      this.modalService.operModal(errorMessages.join('\r\n'));
      return EMPTY;
    }
    return EMPTY;
  }
}
