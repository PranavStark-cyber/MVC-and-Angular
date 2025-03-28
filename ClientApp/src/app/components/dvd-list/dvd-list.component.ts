import { Component, OnInit } from '@angular/core';
import { Dvd, DvdService } from '../../services/dvd.service';

@Component({
  selector: 'app-dvd-list',
  standalone: false,
  templateUrl: './dvd-list.component.html',
  styleUrl: './dvd-list.component.css'
})
export class DvdListComponent implements OnInit {
  dvds: Dvd[] = [];

  constructor(private dvdService: DvdService) {}

  ngOnInit(): void {
    this.dvdService.getAllDvds().subscribe((data) => {
      this.dvds = data;
    });
  }
}
