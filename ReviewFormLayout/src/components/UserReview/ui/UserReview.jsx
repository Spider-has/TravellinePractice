import './UserReview.css';

export const UserReview = ({ avatar, name, review, mark }) => {
    return (
        <article className="user-review-area">
            <img className="user-review-area__avatar" src={avatar} alt="avatar" />
            <div className="user-review-area__review-text-area">
                <h3>{name}</h3>
                <p>{review}</p>
            </div>
            <div className="user-review-area__review-mark">{mark}/5</div>
        </article>
    );
};
