import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { UserBill } from 'src/app/models/userBill';
import { BillsService } from 'src/app/services/bills.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-bill-edit',
  templateUrl: './bill-edit.component.html',
  styleUrls: ['./bill-edit.component.css']
})
export class BillEditComponent implements OnInit {
  userBill?: UserBill

  constructor(private route: ActivatedRoute, private billsService: BillsService, 
    private modalService: ModalService, private router: Router) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadBill(id);
    }
  }

  loadBill(id: string) {
    return this.billsService.getBill(id).subscribe({
      next: userBill => this.userBill = userBill
    });
  }

  approve(id: string) {
    this.billsService.updateBillStatus(id, 1).subscribe({
      next: () => {
        this.modalService.operModal("Η Αίτηση εγκρίθηκε")
        const navigationExtras: NavigationExtras = { state: { listChanged: true} };
        this.router.navigateByUrl('/admin/bills', navigationExtras);
      }
    });
  }

  reject(id: string) {
    this.billsService.updateBillStatus(id, 2).subscribe({
      next: () => {
        this.modalService.operModal("Η Αίτηση απορρίφθηκε")
        const navigationExtras: NavigationExtras = { state: { "listChanged": true} };
        this.router.navigateByUrl('/admin/bills', navigationExtras);
      }
    });
  }

}
