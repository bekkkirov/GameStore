import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {GamesListComponent} from "./components/games-list/games-list.component";
import {GameComponent} from "./components/game/game.component";
import {AddGameComponent} from "./components/add-game/add-game.component";
import {FooterComponent} from "./components/footer/footer.component";

const routes: Routes = [
    {path: "", component: GamesListComponent},
    {path: "games/add", component: AddGameComponent},
    {path: "games/:gameKey", component: GameComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
