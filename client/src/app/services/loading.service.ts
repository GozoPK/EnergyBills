import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  requestsCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  show() {
    this.requestsCount++;
    if (this.requestsCount > 0) {
      this.spinnerService.show(undefined, {
        type: 'ball-spin-clockwise',
        size: 'medium',
        bdColor: "rgba(0, 0, 0, 0.8)",
        color: '#fff',
        fullScreen: true
      });
    }
  }

  hide() {
    this.requestsCount--;
    if (this.requestsCount <= 0) {
      this.requestsCount = 0;
      this.spinnerService.hide();
    }
  }
  
}
