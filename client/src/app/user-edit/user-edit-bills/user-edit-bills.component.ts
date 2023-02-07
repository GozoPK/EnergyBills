import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { UserBill } from 'src/app/models/userBill';
import { UserParams } from 'src/app/models/userParams';
import { ModalService } from 'src/app/services/modal.service';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-user-edit-bills',
  templateUrl: './user-edit-bills.component.html',
  styleUrls: ['./user-edit-bills.component.css']
})
export class UserEditBillsComponent implements OnInit, OnDestroy {
  userParams = new UserParams();

  sub: Subscription = new Subscription();

  private savedBillsChangedSubject = new BehaviorSubject<boolean>(false);
  savedBillsChanged$ = this.savedBillsChangedSubject.asObservable();

  pagedList$ = this.userBillsService.pagedList$;

  constructor(private modalService: ModalService, private userBillsService: UserBillsService) { }

  ngOnInit(): void {
    this.sub = this.savedBillsChanged$.subscribe({
      next: billsChanged => {
        if (billsChanged) {
          this.userParams.state = 'saved';
          this.loadBills(this.userParams);
        }
      }
    });
  }

  loadBills(userParams: UserParams) {
    this.userBillsService.getUserBills(userParams).subscribe({
      next: response => this.userBillsService.setPagedList(response)
    })
  }

  editBill(bill: UserBill) {
    this.modalService.openEditBillModal(bill).subscribe({
      next: billChanged => this.savedBillsChangedSubject.next(billChanged)
    })
  }

  deleteBill(id: string) {
    this.userBillsService.deleteBill(id).subscribe({
      next: () => {
        this.modalService.operModal("Επιτυχής διαγραφή")
        this.savedBillsChangedSubject.next(true);
      }
    });
  }

  pageNext(pageNumber: number) {
    this.userParams.pageNumber = pageNumber;
    this.savedBillsChangedSubject.next(true);
  }

  pagePrevious(pageNumber: number) {
    this.userParams.pageNumber = pageNumber;
    this.savedBillsChangedSubject.next(true);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
