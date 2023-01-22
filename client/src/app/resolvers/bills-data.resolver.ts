import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable} from 'rxjs';
import { PagedList } from '../models/pagingData';
import { UserBill } from '../models/userBill';
import { UserParams } from '../models/userParams';
import { UserBillsService } from '../services/user-bills.service';

@Injectable({
  providedIn: 'root'
})
export class BillsDataResolver implements Resolve<PagedList<UserBill[]>> {

  constructor(private userBillsService: UserBillsService) { }

  resolve(): Observable<PagedList<UserBill[]>> {
    const userParams = new UserParams();
    userParams.state = 'saved';
    return this.userBillsService.getUserBills(userParams);
  }
}
