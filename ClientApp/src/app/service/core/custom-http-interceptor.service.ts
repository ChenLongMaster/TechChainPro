import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MessageService } from "primeng/api";
import { Observable, ReplaySubject, throwError } from "rxjs";
import { catchError, finalize, map, tap } from "rxjs/operators";
import { StorageQuerySerive } from "./storage.query.service";

@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor {

    private _onGoingRequests = 0;
    private _onGoingRequestStatus: ReplaySubject<boolean> = new ReplaySubject<boolean>();

    constructor(private storageQuerySerive:StorageQuerySerive,
                private messageService: MessageService
                ){

    }

    get onGoingRequests(): number {
        return this._onGoingRequests;
    }

    get onGoingRequestStatus(): Observable<boolean> {
        return this._onGoingRequestStatus;
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this._onGoingRequests++;
        if (this._onGoingRequests == 1) {
                this._onGoingRequestStatus.next(true);
        }

        let tokenkey = this.storageQuerySerive.GetToken()
        if(tokenkey){
            req = req.clone(
                {
                    setHeaders:
                    {
                        Authorization: `Bearer ${tokenkey}`
                    }
                }
            )
        }

        return next.handle(req).pipe(
            map(event =>{
                return event;
            }),
            tap(data => {
                console.log(data);
            }),
            catchError(error => {
                if(error.status == 401){
                    this.messageService.add({severity:'error', summary: 'Access Denied', detail: 'You Are Unauthorized.'});
                }
                else{
                    this.messageService.add({severity:'error', summary: error.statusText , detail: error.message});
                }
                return throwError(error)
            }),
            finalize(() => {
                this._onGoingRequests--;
                if (this._onGoingRequests == 0) {
                        this._onGoingRequestStatus.next(false);
                }   
            })
        );
    }
}