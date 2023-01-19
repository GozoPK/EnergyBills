import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.css']
})
export class ConfirmModalComponent implements OnInit {
  text: string = '';
  confirmResult = false;

  constructor(public modalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  confirm() {
    this.confirmResult = true;
    this.modalRef.hide();
  }

}
