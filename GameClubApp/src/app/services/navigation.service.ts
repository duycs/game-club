import { Injectable } from "@angular/core";
import { Location } from "@angular/common";
import { Router, NavigationEnd, ActivatedRoute } from "@angular/router";

@Injectable({ providedIn: "root" })

export class NavigationService {
  showHeader = true;
  isOpenSideMenu = false;
  isOnlyShowPrintLayout = false;
  isChatMobile = false;
  ignoreUrls = ["login", "print"];
  firebaseSession!: any;

  constructor(private router: Router, private activeRoute: ActivatedRoute, private location: Location) {

    if (!this.getHistoryLocal()) {
      localStorage.setItem("history", JSON.stringify([]));
    }

    this.router.events.subscribe((event) => {
      let history = this.getHistoryLocal();
      if (event instanceof NavigationEnd) {
        localStorage.setItem("url", event.url);
        let lastUrl = history.length > 1 ? history[history.length - 1] : "";
        let isIgnoreUrl = this.ignoreUrls.findIndex((e: any) => event.urlAfterRedirects.includes(e.toLocaleLowerCase())) > 0;

        if (lastUrl !== event.urlAfterRedirects && !isIgnoreUrl) {
          history.push(event.urlAfterRedirects);
          this.setHistoryLocal(history);
        }
      }
    });
  }

  initialize() {
    console.log("Initialize NavigationService");
  }

  back(): void {
    let history = this.getHistoryLocal();
    let url = history.length > 1 ? history[history.length - 2] : history[history.length - 1];

    history.pop();
    this.setHistoryLocal(history);

    if (history.length > 0 && url) {
      this.router.navigateByUrl(url);
    } else {
      //this.router.navigateByUrl("/");
    }
  }

  getHistoryLocal() {
    let historyLocal = localStorage.getItem("history");
    if (historyLocal) {
      let history = JSON.parse(historyLocal);
      return history;
    }

    return [];
  }

  setHistoryLocal(history: any[]) {
    localStorage.setItem("history", JSON.stringify(history));
  }
}
