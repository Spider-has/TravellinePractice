import { useCallback, useRef, useState } from 'react';
import type { CardData, ColumnCardsData, Coords } from '../../shared/model';
import { Cards } from '../../shared/data';
import { Column } from '../../shared/components/Column/Column';
import './EventCalendar.css';
import { findCurrentElemIndex } from './utils';

export const EventCalendar = () => {
    const cardsRef = useRef<ColumnCardsData[]>(Cards);
    const currentColumnRef = useRef<number>(0);

    const [, setDraggedNow] = useState(false);
    const currentDragZoneRef = useRef<HTMLDivElement>(null);

    const setNewCurrentDragZone = useCallback((dragZone: HTMLDivElement, elemText: string) => {
        dragZone.style.display = 'block';
        dragZone.innerHTML = elemText;
        if (dragZone != currentDragZoneRef.current) {
            if (currentDragZoneRef.current) currentDragZoneRef.current.style.display = 'none';
            currentDragZoneRef.current = dragZone;
        }
    }, []);

    const onMouseUp = useCallback((currElemData: CardData, elemPosition: Coords) => {
        const columnIndex = currentColumnRef.current;
        let currElem;
        let elemColumn = 0;
        let elemRow = 0;
        for (let i = 0; i < cardsRef.current.length; i++)
            for (let j = 0; j < cardsRef.current[i].cards.length; j++)
                if (cardsRef.current[i].cards[j].id == currElemData.id) {
                    elemColumn = i;
                    elemRow = j;
                    currElem = cardsRef.current[i].cards[j];
                }

        if (currElem) {
            const swapElemIndex = findCurrentElemIndex(
                cardsRef.current[columnIndex].cards,
                currElem,
                elemPosition,
            );

            if (swapElemIndex + 1 !== elemRow && elemColumn == columnIndex) {
                const newCards = [...cardsRef.current[columnIndex].cards];
                newCards.splice(elemRow, 1);
                let indexDiff = 0;
                if (elemRow > swapElemIndex) indexDiff = 1;
                newCards.splice(swapElemIndex + indexDiff, 0, currElem);
                cardsRef.current[columnIndex].cards = newCards;
            } else if (elemColumn != columnIndex) {
                const newCards = [...cardsRef.current];
                newCards[elemColumn].cards.splice(elemRow, 1);
                newCards[columnIndex].cards.splice(swapElemIndex + 1, 0, currElem);
                cardsRef.current = newCards;
            }
            if (currentDragZoneRef.current) {
                currentDragZoneRef.current.style.display = 'none';
                currentDragZoneRef.current = null;
            }
        }
    }, []);

    const onMouseMove = useCallback(
        (currElemData: CardData, elemPosition: Coords) => {
            let currElem;
            let elemRow = 0;
            let elemColumn = 0;
            for (let i = 0; i < cardsRef.current.length; i++)
                for (let j = 0; j < cardsRef.current[i].cards.length; j++)
                    if (cardsRef.current[i].cards[j].id == currElemData.id) {
                        elemRow = j;
                        elemColumn = i;
                        currElem = cardsRef.current[i].cards[j];
                    }

            for (let i = 0; i < cardsRef.current.length; i++) {
                const columnCords = cardsRef.current[i].columnRef?.getBoundingClientRect();
                if (columnCords && elemPosition.x > columnCords.left) currentColumnRef.current = i;
                else break;
            }
            const columnIndex = currentColumnRef.current;

            if (currElem) {
                const swapElemIndex = findCurrentElemIndex(
                    cardsRef.current[columnIndex].cards,
                    currElem,
                    elemPosition,
                );
                if (
                    (swapElemIndex + 1 !== elemRow && columnIndex == elemColumn) ||
                    columnIndex != elemColumn
                ) {
                    // Решил тут напрямую через DOM дерево сделать, потому что позволяет избежать ререндеры
                    if (swapElemIndex >= 0 && cardsRef.current[columnIndex].cards[swapElemIndex].cardDivRef) {
                        const dragZone: HTMLDivElement = cardsRef.current[columnIndex].cards[
                            swapElemIndex
                        ].cardDivRef?.querySelector('.task-card_drop-zone') as HTMLDivElement;
                        if (dragZone) setNewCurrentDragZone(dragZone, currElem.taskText);
                    } else if (cardsRef.current[columnIndex].hightestDropZone) {
                        setNewCurrentDragZone(
                            cardsRef.current[columnIndex].hightestDropZone,
                            currElem.taskText,
                        );
                    }
                } else if (currentDragZoneRef.current) {
                    currentDragZoneRef.current.style.display = 'none';
                    currentDragZoneRef.current = null;
                }
            }
        },
        [setNewCurrentDragZone],
    );
    return (
        <div className="event-calendar-area">
            {cardsRef.current.map((el, i) => (
                <Column
                    key={i}
                    columnName={`week ${i + 1}`}
                    columnCards={el.cards}
                    setCardRef={(cardI: number, el: HTMLDivElement | null) => {
                        if (cardsRef.current[i].cards[cardI])
                            cardsRef.current[i].cards[cardI].cardDivRef = el;
                    }}
                    setColumnRef={(el: HTMLElement | null) => (cardsRef.current[i].columnRef = el)}
                    setHighestDragZoneRef={(el: HTMLDivElement | null) => {
                        cardsRef.current[i].hightestDropZone = el;
                    }}
                    onMouseUp={onMouseUp}
                    onMouseMove={onMouseMove}
                    setDraggedProcess={setDraggedNow}
                />
            ))}
        </div>
    );
};
