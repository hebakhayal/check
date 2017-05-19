import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map' ;


@Injectable()
export class surveyservice {
    constructor(private http: Http) {
           // Call map on the response observable to get the parsed surveyobject
        
      // Subscribe to the observable to get the parsed survey object and attach it to the
      // component
     }

     getAll(){
           this.http.get('http://localhost:36318/Surveys/GetAllServies')
          .map(res => res.json()) ;
     }
}
   
  


 


  
     
