import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((response: HttpErrorResponse) => {
        if (response) {
          switch (response.status) {

            case 400:
              this.toastr.error(response.error, response.status.toString());
              break;

            case 401:
              throw response;

            case 404:
              this.router.navigate(['/not-found']);
              break;

            case 500:
              this.router.navigate(['/server-error']);
              break;

            default:
              this.router.navigate(['/server-error']);
              break;

          }
        }
        throw response;
      })
    );
  }
}
