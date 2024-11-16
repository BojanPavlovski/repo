import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HttpClient, HttpParams } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { inject } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  styles: [`
      .container-fluid{
        width: 250px;
        border: solid black 3px;
        margin-top: 10px;
        padding: 5px;
      }
    `]
})
export class AppComponent {
  title = 'PriceChecker';
  responseData: any;

  
  constructor(private http: HttpClient){

  }

  getData(form: any) : void{
    const { width, height, depth, weight } = form.value;
    
    if (!width || !height || !depth || !weight) {
      console.error('All fields are required.');
      return;
    }

    const endpoint = "http://localhost:5140/api/PackageChecker";

    const params = new HttpParams()
    .set("width", width)
    .set("height", height)
    .set("depth", depth)
    .set("weight", weight)

    this.http.get(endpoint, {params})
    .subscribe(
      (response: any) => {
        this.responseData = response;
        console.log("Response recieved.", this.responseData)
      }
    )
  }
}
