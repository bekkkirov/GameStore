import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import {MatToolbarModule} from "@angular/material/toolbar";
import { GamesListComponent } from './components/games-list/games-list.component';
import {API_BASE_URL} from "./extensions/injection-token";
import {environment} from "../environments/environment";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { FooterComponent } from './components/footer/footer.component';
import { GameComponent } from './components/game/game.component';
import {MatChipsModule} from "@angular/material/chips";
import {AppRoutingModule} from "./app-routing.module";
import {MatIconModule} from "@angular/material/icon";
import {MatMenuModule} from "@angular/material/menu";
import {MatButtonModule} from "@angular/material/button";
import { AddGameComponent } from './components/add-game/add-game.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {ReactiveFormsModule} from "@angular/forms";
import { EditGameComponent } from './components/edit-game/edit-game.component';
import {MatDialogModule} from "@angular/material/dialog";
import { SearchComponent } from './components/search/search.component';
import {SpinnerInterceptor} from "./interceptors/spinner.interceptor";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";

@NgModule({
    declarations: [
        AppComponent,
        NavBarComponent,
        GamesListComponent,
        FooterComponent,
        GameComponent,
        AddGameComponent,
        EditGameComponent,
        SearchComponent,
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        MatToolbarModule,
        HttpClientModule,
        MatChipsModule,
        AppRoutingModule,
        MatIconModule,
        MatMenuModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatProgressSpinnerModule
    ],
    providers:  [
        {
            provide: API_BASE_URL,
            useValue: environment.apiBaseUrl
        },

        {
            provide: HTTP_INTERCEPTORS,
            useClass: SpinnerInterceptor,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
