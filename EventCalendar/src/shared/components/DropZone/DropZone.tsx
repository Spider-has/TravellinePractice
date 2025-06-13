type DropZoneProps = {
    ref: React.Ref<HTMLDivElement | null>;
    text?: string;
};

export const DropZone = (props: DropZoneProps) => {
    return (
        <div ref={props.ref} className={`task-card task-card_drop-zone`}>
            {props.text}
        </div>
    );
};
