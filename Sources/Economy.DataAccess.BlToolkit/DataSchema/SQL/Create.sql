set statement_timeout = 0;
set lock_timeout = 0;
set client_encoding = 'utf8';
set standard_conforming_strings = on;
set check_function_bodies = false;
set client_min_messages = warning;
set row_security = off;

create extension if not exists plpgsql with schema pg_catalog;

comment on extension plpgsql is 'pl/pgsql procedural language';

set search_path = public, pg_catalog;
set default_tablespace = '';
set default_with_oids = false;

-- Table: public.belinvest_course_arhive
-- DROP TABLE public.belinvest_course_arhive;
CREATE TABLE public.belinvest_course_arhive
(
  currency integer NOT NULL, -- валюта
  date timestamp with time zone NOT NULL, -- дата
  buy double precision NOT NULL DEFAULT 0, -- цена покупки
  sel double precision NOT NULL DEFAULT 0, -- цена продажи
  CONSTRAINT pk_belinvest_currency_cur_data PRIMARY KEY (currency, date),
  CONSTRAINT fk_belinvest_currency_id FOREIGN KEY (currency)
      REFERENCES public.currency_type (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.belinvest_course_arhive
  OWNER TO postgres;
COMMENT ON TABLE public.belinvest_course_arhive
  IS 'курсы приведения валют byr-база / курс беливестбанк';
COMMENT ON COLUMN public.belinvest_course_arhive.currency IS 'валюта';
COMMENT ON COLUMN public.belinvest_course_arhive.date IS 'дата';
COMMENT ON COLUMN public.belinvest_course_arhive.buy IS 'цена покупки';
COMMENT ON COLUMN public.belinvest_course_arhive.sel IS 'цена продажи';


-- Table: public.currency_type
-- DROP TABLE public.currency_type;
create sequence currency_id_seq
    start with 1
    increment by 1
    no minvalue
    no maxvalue
    cache 1;
alter table currency_id_seq owner to postgres;


CREATE TABLE public.currency_type
(
  id integer NOT NULL DEFAULT nextval('currency_id_seq'::regclass), -- идентификатор
  name character varying, -- наименование валюты
  short_name character varying(3) NOT NULL, -- трехбуквенное обозначение
  CONSTRAINT pk_currency_id PRIMARY KEY (id)
)
WITH (  OIDS=FALSE);
ALTER TABLE public.currency_type  OWNER TO postgres;
COMMENT ON TABLE public.currency_type  IS 'таблица наименований валют';
COMMENT ON COLUMN public.currency_type.id IS 'идентификатор';
COMMENT ON COLUMN public.currency_type.name IS 'наименование валюты';
COMMENT ON COLUMN public.currency_type.short_name IS 'трехбуквенное обозначение';
	
alter sequence currency_id_seq owned by currency.id;

--
create table montly_report (
    id uuid not null
);
alter table montly_report owner to postgres;
comment on table montly_report is 'пользовательский отчет за месяц';
comment on column montly_report.id is 'идентификатор';

create table transaction (
    id uuid not null,
    registration_date timestamp without time zone not null,
    transaction_date timestamp without time zone,
    code character varying,
    description character varying,
    currency integer not null,
    quantity_by_currency money not null,
    quantity_by_account money,
    commission money,
    from_user_account uuid,
    to_user_account uuid not null,
    montly_report uuid
);

alter table transaction owner to postgres;
comment on column transaction.registration_date is 'дата регистрации транзакции в системе';
comment on column transaction.transaction_date is 'дата совершения транзакции';
comment on column transaction.code is 'код транзакции';
comment on column transaction.description is 'комментарий';
comment on column transaction.currency is 'валюта транзакции';
comment on column transaction.quantity_by_currency is 'сумма в валюте транзакции';
comment on column transaction.quantity_by_account is 'сумма в валюте счета';
comment on column transaction.commission is 'комиссия';
comment on column transaction.from_user_account is 'перевод с счета';
comment on column transaction.to_user_account is 'перевод на счет';
comment on column transaction.id is 'идентификатор';

create table system_user (
    id uuid not null
);
alter table system_user owner to postgres;
comment on column system_user.id is 'идентификатор пользователя';

create table user_account (
    id uuid not null,
    name character varying,
    start_balance money,
    balance money,
    system_user uuid
);
alter table user_account owner to postgres;
comment on table user_account is 'пользовательские счета';
comment on column user_account.id is 'идентификатор';
comment on column user_account.start_balance is 'первоначальный баланс счета';
comment on column user_account.balance is 'итоговое состояние счета';
comment on column user_account.system_user is 'пользователь';

alter table only currency alter column id set default nextval('currency_id_seq'::regclass);

select pg_catalog.setval('currency_id_seq', 1, false);
alter table only byr_currency_conversion_from_belinvest
    add constraint pk_byr_cur_conversion_from_belinvest primary key (date);
alter table only currency
    add constraint pk_currency_id primary key (id);
alter table only montly_report
    add constraint pk_montly_report_id primary key (id);
alter table only transaction
    add constraint pk_transaction_id primary key (id);
alter table only system_user
    add constraint pk_user_id primary key (id);
alter table only user_account
    add constraint pk_user_account_id primary key (id);
alter table only byr_currency_conversion_from_belinvest
    add constraint fk_byr_cur_conv_from_belinvest_currency_id foreign key (currency) references currency(id);
alter table only transaction
    add constraint fk_transaction_currency foreign key (currency) references currency(id);
alter table only transaction
    add constraint fk_transaction_montly_report foreign key (montly_report) references montly_report(id);

revoke all on schema public from public;
revoke all on schema public from postgres;
grant all on schema public to postgres;
grant all on schema public to public;