import { Component, OnInit, AfterViewInit, Input, Output, EventEmitter } from "@angular/core";
import { NavigationEnd } from "@angular/router";
import { Subscription } from "rxjs";
import { AlertService } from "../../services/alert.service";


@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit, AfterViewInit {
    name!: string;

    constructor(
      ) {
    }

    ngAfterViewInit(): void {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
    }

    onToggleSidenav(){

    }
}
