import { Component } from '@angular/core';
import { surveyservice} from '../_services/survey.service';

@Component({
    moduleId: module.id,
    selector: 'survey',
    templateUrl: `survey.component.html`,
    providers: [surveyservice]
})

export class SurveyComponent {
     
     constructor( private _surveyservice : surveyservice) {

       //  _surveyservice.getAll();
     }

 }