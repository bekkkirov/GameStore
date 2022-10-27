import {Component, OnInit, ViewChild} from '@angular/core';
import {GameService} from "../../services/game.service";
import {Genre} from "../../models/genre";
import {PlatformType} from "../../models/platformType";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {EMPTY, mergeMap} from "rxjs";

@Component({
    selector: 'app-add-game',
    templateUrl: './add-game.component.html',
    styleUrls: ['./add-game.component.scss']
})
export class AddGameComponent implements OnInit {
    @ViewChild('imageInput') imageInput;

    genres: Genre[] = [];
    platforms: PlatformType[] = [];
    form: FormGroup = new FormGroup({
        "key": new FormControl(null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
        "name": new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
        "description": new FormControl(null, [Validators.required, Validators.minLength(20), Validators.maxLength(500)]),
        "price": new FormControl(null, [Validators.required, Validators.pattern(/^\d*(?:[.,]\d{1,2})?$/)]),
        "genreIds": new FormControl(null),
        "platformIds": new FormControl(null),
    })

    constructor(private gameService: GameService, private router: Router) {
    }

    ngOnInit(): void {
        this.gameService.getGenres().subscribe(result => this.genres = result);
        this.gameService.getPlatforms().subscribe(result => this.platforms = result);
    }

    addGame() {
        this.gameService.addGame(this.form.getRawValue()).pipe(
            mergeMap((game) => {
                let image = this.imageInput.nativeElement.files[0];

                if(image) {
                    return this.gameService.setImage(game.key, image);
                }

                return EMPTY;
            })
        ).subscribe(r => this.router.navigateByUrl("/"))
    }
}
