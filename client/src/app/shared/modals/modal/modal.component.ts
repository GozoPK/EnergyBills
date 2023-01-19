import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-login-expired-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  imgUrl: string = '';
  text: string = '';

  constructor(public modalRef: BsModalRef) { }

  ngOnInit(): void {
  }

}
