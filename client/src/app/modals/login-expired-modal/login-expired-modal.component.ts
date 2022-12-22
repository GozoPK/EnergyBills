import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-login-expired-modal',
  templateUrl: './login-expired-modal.component.html',
  styleUrls: ['./login-expired-modal.component.css']
})
export class LoginExpiredModalComponent implements OnInit {
  imgUrl: string = '';
  text: string = '';

  constructor(public modalRef: BsModalRef) { }

  ngOnInit(): void {
  }

}
