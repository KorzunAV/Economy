--
-- PostgreSQL database dump
--

-- Dumped from database version 9.5.2
-- Dumped by pg_dump version 9.5.2

-- Started on 2016-10-15 13:32:13

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE economy;
--
-- TOC entry 2158 (class 1262 OID 24921)
-- Name: economy; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE economy WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


ALTER DATABASE economy OWNER TO postgres;

\connect economy

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 2159 (class 0 OID 0)
-- Dependencies: 7
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 1 (class 3079 OID 12355)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2161 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'pl/pgsql procedural language';


SET search_path = public, pg_catalog;

--
-- TOC entry 188 (class 1259 OID 25017)
-- Name: bank_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE bank_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE bank_id_seq OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 187 (class 1259 OID 25009)
-- Name: Bank; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Bank" (
    "Id" integer DEFAULT nextval('bank_id_seq'::regclass) NOT NULL,
    "Name" character varying
);


ALTER TABLE "Bank" OWNER TO postgres;

--
-- TOC entry 2162 (class 0 OID 0)
-- Dependencies: 187
-- Name: COLUMN "Bank"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Bank"."Id" IS 'идентификатор';


--
-- TOC entry 189 (class 1259 OID 25052)
-- Name: CourseArhive; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "CourseArhive" (
    "CurrencyTypeId" integer NOT NULL,
    "RegDate" timestamp with time zone NOT NULL,
    "Buy" double precision DEFAULT 0 NOT NULL,
    "Sel" double precision DEFAULT 0 NOT NULL,
    "CurrencyTypeBaseId" integer NOT NULL,
    "BankId" integer NOT NULL
);


ALTER TABLE "CourseArhive" OWNER TO postgres;

--
-- TOC entry 2163 (class 0 OID 0)
-- Dependencies: 189
-- Name: TABLE "CourseArhive"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "CourseArhive" IS 'курсы приведения валют byr-база / курс беливестбанк';


--
-- TOC entry 2164 (class 0 OID 0)
-- Dependencies: 189
-- Name: COLUMN "CourseArhive"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."CurrencyTypeId" IS 'валюта';


--
-- TOC entry 2165 (class 0 OID 0)
-- Dependencies: 189
-- Name: COLUMN "CourseArhive"."RegDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."RegDate" IS 'дата';


--
-- TOC entry 2166 (class 0 OID 0)
-- Dependencies: 189
-- Name: COLUMN "CourseArhive"."Buy"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."Buy" IS 'цена покупки';


--
-- TOC entry 2167 (class 0 OID 0)
-- Dependencies: 189
-- Name: COLUMN "CourseArhive"."Sel"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."Sel" IS 'цена продажи';


--
-- TOC entry 2168 (class 0 OID 0)
-- Dependencies: 189
-- Name: COLUMN "CourseArhive"."BankId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."BankId" IS 'Банк';


--
-- TOC entry 181 (class 1259 OID 24927)
-- Name: CurrencyType; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "CurrencyType" (
    "Id" integer NOT NULL,
    "Name" character varying,
    "ShortName" character varying(3) NOT NULL
);


ALTER TABLE "CurrencyType" OWNER TO postgres;

--
-- TOC entry 2169 (class 0 OID 0)
-- Dependencies: 181
-- Name: TABLE "CurrencyType"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "CurrencyType" IS 'таблица наименований валют';


--
-- TOC entry 2170 (class 0 OID 0)
-- Dependencies: 181
-- Name: COLUMN "CurrencyType"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."Id" IS 'идентификатор';


--
-- TOC entry 2171 (class 0 OID 0)
-- Dependencies: 181
-- Name: COLUMN "CurrencyType"."Name"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."Name" IS 'наименование валюты';


--
-- TOC entry 2172 (class 0 OID 0)
-- Dependencies: 181
-- Name: COLUMN "CurrencyType"."ShortName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."ShortName" IS 'трехбуквенное обозначение';


--
-- TOC entry 182 (class 1259 OID 24933)
-- Name: MontlyReport; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "MontlyReport" (
    "Id" uuid NOT NULL,
    "StartBalance" money,
    "EndBalance" money,
    "StartDate" timestamp without time zone NOT NULL,
    "WalletId" uuid NOT NULL
);


ALTER TABLE "MontlyReport" OWNER TO postgres;

--
-- TOC entry 2173 (class 0 OID 0)
-- Dependencies: 182
-- Name: TABLE "MontlyReport"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "MontlyReport" IS 'пользовательский отчет за месяц';


--
-- TOC entry 2174 (class 0 OID 0)
-- Dependencies: 182
-- Name: COLUMN "MontlyReport"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."Id" IS 'идентификатор';


--
-- TOC entry 2175 (class 0 OID 0)
-- Dependencies: 182
-- Name: COLUMN "MontlyReport"."StartBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."StartBalance" IS 'баланс на начало месяца';


--
-- TOC entry 2176 (class 0 OID 0)
-- Dependencies: 182
-- Name: COLUMN "MontlyReport"."EndBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."EndBalance" IS 'баланс на конец месяца';


--
-- TOC entry 2177 (class 0 OID 0)
-- Dependencies: 182
-- Name: COLUMN "MontlyReport"."StartDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."StartDate" IS 'Период действия (год месяц)';


--
-- TOC entry 2178 (class 0 OID 0)
-- Dependencies: 182
-- Name: COLUMN "MontlyReport"."WalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."WalletId" IS 'Идентификатор кошелька';


--
-- TOC entry 183 (class 1259 OID 24936)
-- Name: SystemUser; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "SystemUser" (
    "Id" uuid NOT NULL,
    "Name" character varying
);


ALTER TABLE "SystemUser" OWNER TO postgres;

--
-- TOC entry 2179 (class 0 OID 0)
-- Dependencies: 183
-- Name: COLUMN "SystemUser"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "SystemUser"."Id" IS 'идентификатор пользователя';


--
-- TOC entry 184 (class 1259 OID 24942)
-- Name: Transaction; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Transaction" (
    "Id" uuid NOT NULL,
    "RegistrationDate" timestamp without time zone NOT NULL,
    "TransactionDate" timestamp without time zone,
    "Code" character varying,
    "Description" character varying,
    "CurrencyTypeId" integer NOT NULL,
    "QuantityByTransaction" money NOT NULL,
    "QuantityByWallet" money,
    "Commission" money,
    "FromWalletId" uuid,
    "ToWalletId" uuid NOT NULL,
    "MontlyReportId" uuid
);


ALTER TABLE "Transaction" OWNER TO postgres;

--
-- TOC entry 2180 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Id" IS 'идентификатор';


--
-- TOC entry 2181 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."RegistrationDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."RegistrationDate" IS 'дата регистрации транзакции в системе';


--
-- TOC entry 2182 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."TransactionDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."TransactionDate" IS 'дата совершения транзакции';


--
-- TOC entry 2183 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Code"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Code" IS 'код транзакции';


--
-- TOC entry 2184 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Description"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Description" IS 'комментарий';


--
-- TOC entry 2185 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."CurrencyTypeId" IS 'валюта транзакции';


--
-- TOC entry 2186 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."QuantityByTransaction"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."QuantityByTransaction" IS 'сумма в валюте транзакции';


--
-- TOC entry 2187 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."QuantityByWallet"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."QuantityByWallet" IS 'сумма в валюте счета';


--
-- TOC entry 2188 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."Commission"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Commission" IS 'комиссия';


--
-- TOC entry 2189 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."FromWalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."FromWalletId" IS 'перевод с счета';


--
-- TOC entry 2190 (class 0 OID 0)
-- Dependencies: 184
-- Name: COLUMN "Transaction"."ToWalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."ToWalletId" IS 'перевод на счет';


--
-- TOC entry 185 (class 1259 OID 24948)
-- Name: Wallet; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Wallet" (
    "Id" uuid NOT NULL,
    "Name" character varying,
    "StartBalance" money,
    "Balance" money,
    "SystemUserId" uuid,
    "CurrencyTypeId" integer NOT NULL
);


ALTER TABLE "Wallet" OWNER TO postgres;

--
-- TOC entry 2191 (class 0 OID 0)
-- Dependencies: 185
-- Name: TABLE "Wallet"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "Wallet" IS 'пользовательские счета';


--
-- TOC entry 2192 (class 0 OID 0)
-- Dependencies: 185
-- Name: COLUMN "Wallet"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."Id" IS 'идентификатор';


--
-- TOC entry 2193 (class 0 OID 0)
-- Dependencies: 185
-- Name: COLUMN "Wallet"."StartBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."StartBalance" IS 'первоначальный баланс счета';


--
-- TOC entry 2194 (class 0 OID 0)
-- Dependencies: 185
-- Name: COLUMN "Wallet"."Balance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."Balance" IS 'итоговое состояние счета';


--
-- TOC entry 2195 (class 0 OID 0)
-- Dependencies: 185
-- Name: COLUMN "Wallet"."SystemUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."SystemUserId" IS 'пользователь';


--
-- TOC entry 2196 (class 0 OID 0)
-- Dependencies: 185
-- Name: COLUMN "Wallet"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."CurrencyTypeId" IS 'валюта кошелька';


--
-- TOC entry 186 (class 1259 OID 24954)
-- Name: currency_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE currency_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE currency_id_seq OWNER TO postgres;

--
-- TOC entry 2197 (class 0 OID 0)
-- Dependencies: 186
-- Name: currency_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE currency_id_seq OWNED BY "CurrencyType"."Id";


--
-- TOC entry 2012 (class 2604 OID 24956)
-- Name: Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CurrencyType" ALTER COLUMN "Id" SET DEFAULT nextval('currency_id_seq'::regclass);


--
-- TOC entry 2027 (class 2606 OID 25016)
-- Name: pk_bank_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Bank"
    ADD CONSTRAINT pk_bank_id PRIMARY KEY ("Id");


--
-- TOC entry 2029 (class 2606 OID 25075)
-- Name: pk_course_arhive_bank_currency_type_reg_date; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT pk_course_arhive_bank_currency_type_reg_date PRIMARY KEY ("BankId", "CurrencyTypeId", "RegDate");


--
-- TOC entry 2017 (class 2606 OID 24960)
-- Name: pk_currency_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CurrencyType"
    ADD CONSTRAINT pk_currency_id PRIMARY KEY ("Id");


--
-- TOC entry 2019 (class 2606 OID 24962)
-- Name: pk_montly_report_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "MontlyReport"
    ADD CONSTRAINT pk_montly_report_id PRIMARY KEY ("Id");


--
-- TOC entry 2023 (class 2606 OID 24964)
-- Name: pk_transaction_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT pk_transaction_id PRIMARY KEY ("Id");


--
-- TOC entry 2025 (class 2606 OID 24966)
-- Name: pk_user_account_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT pk_user_account_id PRIMARY KEY ("Id");


--
-- TOC entry 2021 (class 2606 OID 24968)
-- Name: pk_user_id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SystemUser"
    ADD CONSTRAINT pk_user_id PRIMARY KEY ("Id");


--
-- TOC entry 2039 (class 2606 OID 25069)
-- Name: fk_coursearhive_bank; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT fk_coursearhive_bank FOREIGN KEY ("BankId") REFERENCES "Bank"("Id") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2037 (class 2606 OID 25059)
-- Name: fk_coursearhive_currencytype; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT fk_coursearhive_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 2038 (class 2606 OID 25064)
-- Name: fk_coursearhive_currencytypebase; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT fk_coursearhive_currencytypebase FOREIGN KEY ("CurrencyTypeBaseId") REFERENCES "CurrencyType"("Id") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2031 (class 2606 OID 24974)
-- Name: fk_transaction_currency; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transaction_currency FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 2032 (class 2606 OID 24979)
-- Name: fk_transaction_montly_report; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transaction_montly_report FOREIGN KEY ("MontlyReportId") REFERENCES "MontlyReport"("Id");


--
-- TOC entry 2033 (class 2606 OID 24984)
-- Name: fk_transactionfromwallet_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transactionfromwallet_wallet FOREIGN KEY ("FromWalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 2034 (class 2606 OID 24989)
-- Name: fk_transactiontowallet_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transactiontowallet_wallet FOREIGN KEY ("ToWalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 2035 (class 2606 OID 24994)
-- Name: fk_wallet_currencytype; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT fk_wallet_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 2036 (class 2606 OID 24999)
-- Name: fk_wallet_systemuser; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT fk_wallet_systemuser FOREIGN KEY ("SystemUserId") REFERENCES "SystemUser"("Id");


--
-- TOC entry 2030 (class 2606 OID 25004)
-- Name: pk_montlyreport_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "MontlyReport"
    ADD CONSTRAINT pk_montlyreport_wallet FOREIGN KEY ("WalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 2160 (class 0 OID 0)
-- Dependencies: 7
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2016-10-15 13:32:13

--
-- PostgreSQL database dump complete
--

