const reviewsKey = 'reviews';

if (!localStorage.getItem(reviewsKey)) localStorage.setItem(reviewsKey, JSON.stringify([]));

export const AddNewReview = newReview => {
    const data = localStorage.getItem(reviewsKey);
    const reviews = JSON.parse(data);
    const newData = [newReview, ...reviews];
    localStorage.setItem(reviewsKey, JSON.stringify(newData));
};

export const GetAllReviews = () => {
    return JSON.parse(localStorage.getItem(reviewsKey));
};
