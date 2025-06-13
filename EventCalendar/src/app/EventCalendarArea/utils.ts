import type { CardsDomData, Coords } from '../../shared/model';

export const findCurrentElemIndex = (cards: CardsDomData[], currElem: CardsDomData, elemPosition: Coords) => {
    let index = -1;
    for (let i = 0; i < cards.length; i++) {
        const el = cards[i];
        if (el.id != currElem.id && el.cardDivRef != null && currElem.cardDivRef != null) {
            const elCoords = el.cardDivRef.getBoundingClientRect();
            if (elemPosition.y > elCoords.top) index = i;
            else break;
        }
    }
    return index;
};
