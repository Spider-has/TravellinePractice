import { UserReview } from '../../../components/UserReview';
import { GetAllReviews } from '../../../utils/localStorageLogic';
import './UserReviewsArea.css';

export const UserReviewsArea = () => {
    const reviews = GetAllReviews();
    return (
        <section className="users-reviews">
            {reviews.map(el => (
                <UserReview key={el.id} {...el} />
            ))}
        </section>
    );
};
