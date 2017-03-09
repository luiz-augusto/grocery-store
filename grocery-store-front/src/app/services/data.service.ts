import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { UpdateGroceriesPosition } from "app/models/updateGroceriesPosition";

@Injectable()
export class DataService {
    private serviceUrl: string = 'http://localhost:54382/';    

    constructor(private http: Http) { }

    getGroceriesList() {
        return this.http
            .get(this.serviceUrl + "v1/groceries/")
            .map((res: Response) => res.json());
    }

    getUnitiesList(){
        return this.http
            .get(this.serviceUrl + 'v1/unities')
            .map((res: Response) => res.json());
    }

    getCategoriesList(){
        return this.http
            .get(this.serviceUrl + 'v1/categories')
            .map((res: Response) => res.json());
    }

    getItem(id){
        return this.http
            .get(this.serviceUrl + 'v1/groceries/' + id)
            .map((res: Response) => res.json());
    }

    insert(groceries){
        return this.http
            .post(this.serviceUrl + 'v1/groceries/', groceries)
            .map((res: Response) => res.json());
    }

    update(groceries){
        return this.http
            .put(this.serviceUrl + 'v1/groceries/' + groceries.id, groceries)
            .map((res: Response) => res.json());
    }

    updatePosition(id: string, position: number){
        var updateGroceriesPosition = new UpdateGroceriesPosition(id, position);
        return this.http
            .put(this.serviceUrl + 'v1/groceries/position/' + id, updateGroceriesPosition)
            .map((res: Response) => res.json());
    }

    delete(id){
        return this.http
            .delete(this.serviceUrl + 'v1/groceries/' + id)
            .map((res: Response) => res.json());
    }

    createOrder(data: any) {
        var token = localStorage.getItem('mws.token');
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Authorization', `Bearer ${token}`); Headers
        let options = new RequestOptions({ headers: headers });
        return this.http
            .post(this.serviceUrl + 'v1/orders', data, options)
            .map((res: Response) => res.json());
    }
}