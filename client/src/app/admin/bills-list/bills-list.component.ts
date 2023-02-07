import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { UserParams } from 'src/app/models/userParams';
import { BillsService } from 'src/app/services/bills.service';

@Component({
  selector: 'app-bills-list',
  templateUrl: './bills-list.component.html',
  styleUrls: ['./bills-list.component.css']
})
export class BillsListComponent implements OnInit, OnDestroy {
  userParams = new UserParams();

  pagedList$ = this.billsService.pagedList$;

  sub = new Subscription();

  private billsChangedSubject = new BehaviorSubject<boolean>(false);
  billsChanged$ = this.billsChangedSubject.asObservable();

  constructor(private billsService: BillsService, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const routerState = navigation?.extras?.state?.['listChanged'];
    if (routerState) this.billsChangedSubject.next(routerState);
  }

  ngOnInit(): void {
    this.sub = this.billsChanged$.subscribe({
      next: billsChanged => {
        if (billsChanged) {
          this.loadBills(this.userParams);
        }
      }
    })
  }

  loadBills(userParams: UserParams) {
    this.billsService.getBills(userParams).subscribe({
      next: response => this.billsService.setPagedList(response)
    })
  }

  pageNext(pageNumber: number) {
    this.userParams.pageNumber = pageNumber;
    this.billsChangedSubject.next(true);
  }

  pagePrevious(pageNumber: number) {
    this.userParams.pageNumber = pageNumber;
    this.billsChangedSubject.next(true);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
