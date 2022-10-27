import {Component, OnInit} from '@angular/core';
import {GameService} from "../../services/game.service";
import {Game} from "../../models/game";
import {MatDialog} from "@angular/material/dialog";
import {EditGameComponent} from "../edit-game/edit-game.component";
import {ActivatedRoute, ParamMap} from "@angular/router";
import {GameSearchOptions} from "../../models/game-search-options";

@Component({
    selector: 'app-games-list',
    templateUrl: './games-list.component.html',
    styleUrls: ['./games-list.component.scss']
})
export class GamesListComponent implements OnInit {
    games: Game[] = [];
    searchOptions: GameSearchOptions = new GameSearchOptions();

    constructor(private gameService: GameService,
                private editDialog: MatDialog,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.queryParamMap.subscribe(params => {
            if(params.keys.length > 0) {
                this.getGamesWithFilters(params);
            }

            else {
                this.getAllGames();
            }
        })
    }

    getGamesWithFilters(params: ParamMap) {
        this.searchOptions.namePattern = params.get('namePattern');
        this.searchOptions.genreIds = params.getAll('genreIds').map(id => +id);

        this.gameService.search(this.searchOptions).subscribe(games => {
            this.games = games;
        });
    }

    getAllGames() {
        this.gameService.getGames().subscribe(result => this.games = result);
    }

    deleteGame(gameId: number) {
        this.gameService.deleteGame(gameId).subscribe(r => {
            let index = this.games.findIndex(g => g.id == gameId);

            if(index > -1) {
                this.games.splice(index, 1);
            }
        });
    }

    openEditDialog(game: Game) {
        this.editDialog.open(EditGameComponent, {
            height: '90vh',
            panelClass: 'edit-dialog',
            backdropClass: 'edit-backdrop',
            data: game
        });
    }
}
