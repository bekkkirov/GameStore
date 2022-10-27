import {Component, OnInit} from '@angular/core';
import {GameService} from "../../services/game.service";
import {Game} from "../../models/game";
import {ActivatedRoute} from "@angular/router";

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {
    game: Game;

    constructor(private gameService: GameService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.getGame();
    }

    getGame() {
        let key = this.route.snapshot.paramMap.get('gameKey');

        this.gameService.getGame(key).subscribe(result => this.game = result);
    }
}
