import { SubmitInput } from '../../../components/SubmitInput/SubmitInput';
import { TextArea } from '../../../components/TextArea/TextArea';
import { TextInput } from '../../../components/TextInput/TextInput';
import './ReviewForm.css';
import { RangeInput } from '../../../components/RangeInput/RangeInput';
import { useEffect, useRef } from 'react';
import { AddNewReview } from '../../../utils/localStorageLogic';
import { witchDoctorAvatar } from '../../../static';

// Написал логику формы именно так, потому что хотел сделать вообще без ререндеров, специально поставил такую цель
export const ReviewForm = () => {
    const clearRef = useRef(null);
    const speedRef = useRef(null);
    const serviceRef = useRef(null);
    const placeRef = useRef(null);
    const cultureRef = useRef(null);
    const nameRef = useRef(null);
    const reviewRef = useRef(null);

    const submitRef = useRef(null);

    const formRef = useRef(null);

    const onChangeValidityCheck = () => {
        if (
            clearRef.current &&
            speedRef.current &&
            serviceRef.current &&
            placeRef.current &&
            cultureRef.current &&
            nameRef.current &&
            reviewRef.current &&
            nameRef.current.trim() !== '' &&
            reviewRef.current.trim() !== ''
        ) {
            submitRef.current.disabled = false;
        } else submitRef.current.disabled = true;
    };

    const setRefValue = ref => {
        return value => {
            ref.current = value;
            onChangeValidityCheck();
        };
    };

    const setInputValue = ref => {
        return event => {
            ref.current = event.target.value;
            onChangeValidityCheck();
        };
    };

    useEffect(() => {
        const onSubmit = () => {
            const mark =
                (clearRef.current +
                    speedRef.current +
                    serviceRef.current +
                    placeRef.current +
                    cultureRef.current) /
                5;

            AddNewReview({
                id: Math.floor(Math.random() * 100000),
                name: nameRef.current,
                review: reviewRef.current,
                avatar: witchDoctorAvatar,
                mark: mark,
            });
        };
        formRef.current.addEventListener('submit', onSubmit);
        () => formRef.current.removeEventListener('submit', onSubmit);
    }, []);

    return (
        <form ref={formRef} className="review-form-area">
            <h1>Помогите нам сделать процесс бронирования лучше</h1>
            <RangeInput setValue={setRefValue(clearRef)} label={'Чистенько'} />
            <RangeInput setValue={setRefValue(serviceRef)} label={'Сервис'} />
            <RangeInput setValue={setRefValue(speedRef)} label={'Скорость'} />
            <RangeInput setValue={setRefValue(placeRef)} label={'Место'} />
            <RangeInput setValue={setRefValue(cultureRef)} label={'Культура речи'} />
            <TextInput setValue={setInputValue(nameRef)} labelText={'*Имя'} placeholder={'Как вас зовут?'} />
            <TextArea
                setValue={setInputValue(reviewRef)}
                placeholder={'Напишите, что понравилось, что было непонятно'}
            />
            <SubmitInput ref={submitRef} text={'Отправить'} />
        </form>
    );
};
