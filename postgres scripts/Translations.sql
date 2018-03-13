INSERT INTO cultures ("code", "name") VALUES
('en-us', 'English'),
('es-mx', 'Español')


INSERT INTO translations ("dtmCreated", "keyCultureId", "key", "dtmUpdated", "tranCultureId", "value") VALUES
(now(), 1, 'Household Name', now(), 2, 'Familia'),
(now(), 1, 'Home Phone', now(), 2, 'Teléfono de casa'),
(now(), 1, 'Street Address', now(), 2, 'Dirección'),
(now(), 1, 'City', now(), 2, 'Ciudad'),
(now(), 1, 'State', now(), 2, 'Estado'),
(now(), 1, 'Zip', now(), 2, 'Codigo'),
(now(), 1, 'Adult 1', now(), 2, 'Adulto 1'),
(now(), 1, 'Adult 2', now(), 2, 'Adulto 2'),
(now(), 1, 'Email Address', now(), 2, 'correo electronico'),
(now(), 1, 'Mobile Phone', now(), 2, 'Teléfono móvil')