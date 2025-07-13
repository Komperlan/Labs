CREATE TABLE Owner (
                       id SERIAL PRIMARY KEY,
                       name VARCHAR(255) NOT NULL,
                       birthdate DATE NOT NULL
);

CREATE TYPE color_enum AS ENUM ('BLACK', 'WHITE', 'GRAY', 'ORANGE', 'MIXED');

CREATE TABLE pet (
                     id SERIAL PRIMARY KEY,
                     name VARCHAR(255) NOT NULL,
                     birthdate DATE NOT NULL,
                     breed VARCHAR(255),
                     color color_enum NOT NULL,
                     owner_id INTEGER NOT NULL,
                     CONSTRAINT fk_owner FOREIGN KEY (owner_id) REFERENCES Owner(id)
);

CREATE TABLE pet_friends (
                             pet_id INTEGER NOT NULL,
                             friend_id INTEGER NOT NULL,
                             PRIMARY KEY (pet_id, friend_id),
                             FOREIGN KEY (pet_id) REFERENCES pet(id),
                             FOREIGN KEY (friend_id) REFERENCES pet(id)
);