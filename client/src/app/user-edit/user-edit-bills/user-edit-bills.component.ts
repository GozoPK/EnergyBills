import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { PagedList } from 'src/app/models/pagingData';
import { UserBill } from 'src/app/models/userBill';
import { UserParams } from 'src/app/models/userParams';
import { ModalService } from 'src/app/services/modal.service';
import { UserBillsService } from 'src/app/services/user-bills.service';

@Component({
  selector: 'app-user-edit-bills',
  templateUrl: './user-edit-bills.component.html',
  styleUrls: ['./user-edit-bills.component.css']
})
export class UserEditBillsComponent implements OnInit {
  pagedList = new PagedList<UserBill[]>();
  userParams = new UserParams();

  savedBillsChangedSubject = new BehaviorSubject<boolean>(false);
  savedBillsChanged$ = this.savedBillsChangedSubject.asObservable();

  constructor(private route: ActivatedRoute, private modalService: ModalService, private userBillsService: UserBillsService) { }

  ngOnInit(): void {
    this.route.parent?.data.subscribe({
      next: data => {
        this.pagedList = data['savedBills'];
      }
    })

    this.savedBillsChanged$.subscribe({
      next: billsChanged => {
        if (billsChanged) {
          this.userParams.state = 'saved';
          this.loadBills(this.userParams);
        }
      }
    })
  }

  loadBills(userParams: UserParams) {
    this.userBillsService.getUserBills(userParams).subscribe({
      next: response => this.pagedList = response
    })
  }

  editBill(bill: UserBill) {
    this.modalService.openEditBillModal(bill).subscribe({
      next: billChanged => this.savedBillsChangedSubject.next(billChanged)
    })
  }

  deleteBill(id: string) {
    const index = this.pagedList.result!.findIndex(bill => bill.id === id);
    this.userBillsService.deleteBill(id).subscribe({
      next: () => {
        this.modalService.operModal("Επιτυχής διαγραφή")
        this.pagedList.result?.splice(index, 1);
      }
    });
  }

  pageNext() {
    if (this.pagedList.pagination) {
      this.userParams.pageNumber = this.pagedList.pagination?.currentPage + 1;
      this.savedBillsChangedSubject.next(true);
    }
  }

  pagePrevious() {
    if (this.pagedList.pagination) {
      this.userParams.pageNumber = this.pagedList.pagination?.currentPage - 1;
      this.savedBillsChangedSubject.next(true);
    }
  }

}
