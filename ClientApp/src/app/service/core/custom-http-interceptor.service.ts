import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, ReplaySubject } from "rxjs";
import { finalize } from "rxjs/operators";
import { StorageQuerySerive } from "./storage.query.service";

@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor {

    private _onGoingRequests = 0;
    private _onGoingRequestStatus: ReplaySubject<boolean> = new ReplaySubject<boolean>();

    constructor(private storageQuerySerive:StorageQuerySerive){

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
            finalize(() => {
                this._onGoingRequests--;
                if (this._onGoingRequests == 0) {
                        this._onGoingRequestStatus.next(false);
                }   
            })
        );
    }
}