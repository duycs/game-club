import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PageEvent, MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { ActivatedRoute, Router } from "@angular/router";
import { ClubDataSource } from "./club-data-source";
import { GameClubService } from "../../../services/game-club.service";


@Component({
    selector: 'app-club-list',
    templateUrl: './club-list.component.html',
})

export class ClubListComponent implements OnInit {
    id: any = '';
    length = 50;
    pageSize = 10;
    pageIndex = 1;
    pageSizeOptions = [5, 10, 15, 20];
    pageEvent!: PageEvent;
    queryParams!: any;
    allOptions: any[] = [
        {
          id: "id",
          name: "Id",
          type: "number",
          select: "",
          values: "",
          viewValues: "",
          isSelected: false
        },
        {
            id: "name",
            name: "Name",
            type: "text",
            select: "",
            values: "",
            viewValues: "",
            isSelected: false
        },
        {
            id: "description",
            name: "Description",
            type: "text",
            select: "",
            values: "",
            viewValues: "",
            isSelected: false
        },
        {
            id: "text",
            name: "Text",
            type: "text",
            select: "",
            values: "",
            viewValues: "",
            isSelected: false
        }

    ];

    hidePageSize = false;
    showPageSizeOptions = true;
    showFirstLastButtons = true;
    disabled = false;

    searchKeywords: any[] = [];

    displayedColumns: string[] = ['id', 'name', 'description', 'action'];

    dataSource!: ClubDataSource;

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    constructor(
        private ganeClubService: GameClubService,
        private router: Router,
        private activeRoute: ActivatedRoute) {
        this.dataSource = new ClubDataSource(this.ganeClubService);
    }

    ngAfterViewInit() {
    }

    ngOnInit(): void {
        this.dataSource.loadData(this.getQueryParams());
    }

    getQueryParams() {
        let queryParams: any = {};

        if (this.activeRoute.snapshot.queryParams["id"]) queryParams.id = this.activeRoute.snapshot.queryParams["id"];
        if (this.activeRoute.snapshot.queryParams["name"]) queryParams.name = this.activeRoute.snapshot.queryParams["name"];
        if (this.activeRoute.snapshot.queryParams["description"]) queryParams.description = this.activeRoute.snapshot.queryParams["description"];
        if (this.activeRoute.snapshot.queryParams["text"]) queryParams.text = this.activeRoute.snapshot.queryParams["text"];

        return queryParams;
    }

    updateSearch(event: any) {
        this.dataSource.loadData(this.getQueryParams());
    }

    loadPage() {
        this.dataSource.loadData(
            this.getQueryParams(),
            this.paginator.pageIndex,
            this.paginator.pageSize
        );
    }

    handlePageEvent(e: PageEvent) {
        this.pageEvent = e;
        this.length = e.length;
        this.pageSize = e.pageSize;
        this.pageIndex = e.pageIndex;

        this.loadPage();
    }

    addClub() {
        this.router.navigateByUrl('/clubs/create-club');
    }


    openDetail(element: any){
        this.router.navigateByUrl(`/clubs/${element.id}`);
    }

}
