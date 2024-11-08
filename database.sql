create database tours
    with owner postgres;

create table public.users
(
    user_id    uuid                                not null
        primary key,
    name       varchar(255)                        not null,
    email      varchar(255)                        not null
        unique,
    password   varchar(255)                        not null,
    created_at timestamp default CURRENT_TIMESTAMP not null,
    role       varchar(5)                          not null
);

alter table public.users
    owner to postgres;

create table public.tours
(
    tour_id     uuid                                not null
        primary key,
    name        varchar(255)                        not null,
    description text                                not null,
    country     varchar(255)                        not null,
    duration    integer                             not null,
    created_at  timestamp default CURRENT_TIMESTAMP not null,
    basic_price money                               not null
);

alter table public.tours
    owner to postgres;

create table public.tour_options
(
    option_id        uuid                                not null
        primary key,
    tour_id          uuid                                not null
        references public.tours
            on delete cascade,
    option_type      varchar(255)                        not null,
    description      text                                not null,
    default_value    boolean   default false             not null,
    additional_price numeric(10, 2)                      not null,
    created_at       timestamp default CURRENT_TIMESTAMP not null
);

alter table public.tour_options
    owner to postgres;

create table public.user_favorites
(
    favorite_id uuid                                not null
        primary key,
    user_id     uuid                                not null
        references public.users
            on delete cascade,
    tour_id     uuid                                not null
        references public.tours
            on delete cascade,
    created_at  timestamp default CURRENT_TIMESTAMP not null
);

alter table public.user_favorites
    owner to postgres;

create table public.bookings
(
    booking_id     uuid                                not null
        primary key,
    user_id        uuid                                not null
        references public.users
            on delete cascade,
    tour_id        uuid                                not null
        references public.tours
            on delete cascade,
    total_price    numeric(10, 2)                      not null,
    booking_status varchar(255)                        not null,
    created_at     timestamp default CURRENT_TIMESTAMP not null
);

alter table public.bookings
    owner to postgres;

create table public.booking_options
(
    booking_option_id uuid                                not null
        primary key,
    booking_id        uuid                                not null
        references public.bookings
            on delete cascade,
    option_id         uuid                                not null
        references public.tour_options
            on delete cascade,
    created_at        timestamp default CURRENT_TIMESTAMP not null
);

alter table public.booking_options
    owner to postgres;

create table public.reviews
(
    review_id  uuid                                not null
        primary key,
    user_id    uuid                                not null
        references public.users
            on delete cascade,
    tour_id    uuid                                not null
        references public.tours
            on delete cascade,
    rating     double precision                    not null
        constraint reviews_rating_check
            check ((rating >= (1)::double precision) AND (rating <= (5)::double precision)),
    comment    text                                not null,
    created_at timestamp default CURRENT_TIMESTAMP not null,
    status     varchar                             not null
);

alter table public.reviews
    owner to postgres;

