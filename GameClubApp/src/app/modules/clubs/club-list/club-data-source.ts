import { CollectionViewer, DataSource } from "@angular/cdk/collections"
import { BehaviorSubject, catchError, finalize, Observable, of } from "rxjs";
import { GameClubService } from "../../../services/game-club.service";

export class ClubDataSource implements DataSource<any>{
    private dataSubject = new BehaviorSubject<any[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();
    public count!: number;

    constructor(private gameClubService: GameClubService
    ) { }

    connect(collectionViewer: CollectionViewer): Observable<any[]> {
        return this.dataSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.dataSubject.complete();
        this.loadingSubject.complete();
    }

    loadData(queryParams: any, pageIndex = 0, pageSize = 10) {
        this.loadingSubject.next(true);
        this.gameClubService.getClubs(queryParams, pageIndex, pageSize).pipe(
            catchError(() => of()),
            finalize(() => this.loadingSubject.next(false))
        )
            .subscribe(paggedData => {
                this.dataSubject.next(paggedData);
                this.count = paggedData.length;
            });
    }
}
