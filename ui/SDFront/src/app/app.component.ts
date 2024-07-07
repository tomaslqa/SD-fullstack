import { Component, inject } from '@angular/core';
import { HttpClient, HttpHeaders, provideHttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'SDfront';
  readonly APIUrl = "http://localhost:5151/api/SDAppControler/";
  http = inject(HttpClient);
  medicine: any = [];

  onRefresh() {
    this.http.get(this.APIUrl + 'GetMedicine').subscribe(data => {
      this.medicine = data;
      console.log(this.medicine)
    });
  }

  onSubmit() {
    var formData = new FormData();
    var code = (<HTMLInputElement>document.getElementById("code")).value;
    formData.append("newCode", code);
    var name = (<HTMLInputElement>document.getElementById("name")).value;
    formData.append("newMedicine", name);
    var description = (<HTMLInputElement>document.getElementById("description")).value;
    formData.append("newDescr", description);
    var image = (<HTMLInputElement>document.getElementById("image")).value;
    formData.append("newImg", image);
    var dosing = (<HTMLInputElement>document.getElementById("dosing")).value;
    formData.append("newDose", dosing);
    this.http.post(this.APIUrl + 'AddMedicine', formData).subscribe(data => {
      this.medicine = data;
      console.log(this.medicine);
    });
  }

  onDelete() {
    var id = (<HTMLInputElement>document.getElementById("delete")).value;
    this.http.delete(this.APIUrl + 'DeleteMedicine?id=' + id).subscribe();
  }

  ngOnInit() {
    this.onRefresh();
  }

}