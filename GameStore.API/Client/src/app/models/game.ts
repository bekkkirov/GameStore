import {Image} from "./image";
import {Genre} from "./genre";
import {PlatformType} from "./platformType";

export interface Game {
    id: number;
    key: string;
    name: string;
    description: string;
    price: number;
    image: Image;
    genres: Genre[];
    platformTypes: PlatformType[];
}
