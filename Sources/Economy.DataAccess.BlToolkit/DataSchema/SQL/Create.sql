--
-- PostgreSQL database dump
--

-- Dumped from database version 9.3.5
-- Dumped by pg_dump version 9.3.5
-- Started on 2016-09-07 16:45:16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- TOC entry 179 (class 3079 OID 11750)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 1996 (class 0 OID 0)
-- Dependencies: 179
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'pl/pgsql procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 177 (class 1259 OID 6175356)
-- Name: Bank; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Bank" (
    "Id" integer NOT NULL,
    "Name" character varying
);


ALTER TABLE public."Bank" OWNER TO postgres;

--
-- TOC entry 178 (class 1259 OID 6175359)
-- Name: Bank_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Bank_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Bank_Id_seq" OWNER TO postgres;

--
-- TOC entry 1997 (class 0 OID 0)
-- Dependencies: 178
-- Name: Bank_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Bank_Id_seq" OWNED BY "Bank"."Id";


--
-- TOC entry 176 (class 1259 OID 6175344)
-- Name: CourseArhive; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "CourseArhive" (
    "CurrencyTypeId" integer NOT NULL,
    "RegDate" timestamp with time zone NOT NULL,
    "CurrencyTypeBaseId" integer NOT NULL,
    "BankId" integer NOT NULL,
    "Buy" money DEFAULT 0 NOT NULL,
    "Sel" money DEFAULT 0 NOT NULL
);


ALTER TABLE public."CourseArhive" OWNER TO postgres;

--
-- TOC entry 1998 (class 0 OID 0)
-- Dependencies: 176
-- Name: TABLE "CourseArhive"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "CourseArhive" IS 'Р С”РЎС“РЎР‚РЎРѓРЎвЂ№ Р С—РЎР‚Р С‘Р Р†Р ВµР Т‘Р ВµР Р…Р С‘РЎРЏ Р Р†Р В°Р В»РЎР‹РЎвЂљ byr-Р В±Р В°Р В·Р В° / Р С”РЎС“РЎР‚РЎРѓ Р В±Р ВµР В»Р С‘Р Р†Р ВµРЎРѓРЎвЂљР В±Р В°Р Р…Р С”';


--
-- TOC entry 1999 (class 0 OID 0)
-- Dependencies: 176
-- Name: COLUMN "CourseArhive"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."CurrencyTypeId" IS 'Р Р†Р В°Р В»РЎР‹РЎвЂљР В°';


--
-- TOC entry 2000 (class 0 OID 0)
-- Dependencies: 176
-- Name: COLUMN "CourseArhive"."RegDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."RegDate" IS 'Р Т‘Р В°РЎвЂљР В°';


--
-- TOC entry 2001 (class 0 OID 0)
-- Dependencies: 176
-- Name: COLUMN "CourseArhive"."CurrencyTypeBaseId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."CurrencyTypeBaseId" IS 'РўРёРї Р±Р°Р·РѕРІРѕР№ РІР°Р»СЋС‚С‹';


--
-- TOC entry 2002 (class 0 OID 0)
-- Dependencies: 176
-- Name: COLUMN "CourseArhive"."BankId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."BankId" IS 'РРґРµРЅС‚РёС„РёРєР°С‚РѕСЂ Р±Р°РЅРєР°';


--
-- TOC entry 2003 (class 0 OID 0)
-- Dependencies: 176
-- Name: COLUMN "CourseArhive"."Buy"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."Buy" IS 'РЎвЂ Р ВµР Р…Р В° Р С—Р С•Р С”РЎС“Р С—Р С”Р С‘';


--
-- TOC entry 2004 (class 0 OID 0)
-- Dependencies: 176
-- Name: COLUMN "CourseArhive"."Sel"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CourseArhive"."Sel" IS 'РЎвЂ Р ВµР Р…Р В° Р С—РЎР‚Р С•Р Т‘Р В°Р В¶Р С‘';


--
-- TOC entry 170 (class 1259 OID 6175250)
-- Name: CurrencyType; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "CurrencyType" (
    "Id" integer NOT NULL,
    "Name" character varying,
    "ShortName" character varying(3) NOT NULL
);


ALTER TABLE public."CurrencyType" OWNER TO postgres;

--
-- TOC entry 2005 (class 0 OID 0)
-- Dependencies: 170
-- Name: TABLE "CurrencyType"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "CurrencyType" IS 'РЎвЂљР В°Р В±Р В»Р С‘РЎвЂ Р В° Р Р…Р В°Р С‘Р СР ВµР Р…Р С•Р Р†Р В°Р Р…Р С‘Р в„– Р Р†Р В°Р В»РЎР‹РЎвЂљ';


--
-- TOC entry 2006 (class 0 OID 0)
-- Dependencies: 170
-- Name: COLUMN "CurrencyType"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."Id" IS 'Р С‘Р Т‘Р ВµР Р…РЎвЂљР С‘РЎвЂћР С‘Р С”Р В°РЎвЂљР С•РЎР‚';


--
-- TOC entry 2007 (class 0 OID 0)
-- Dependencies: 170
-- Name: COLUMN "CurrencyType"."Name"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."Name" IS 'Р Р…Р В°Р С‘Р СР ВµР Р…Р С•Р Р†Р В°Р Р…Р С‘Р Вµ Р Р†Р В°Р В»РЎР‹РЎвЂљРЎвЂ№';


--
-- TOC entry 2008 (class 0 OID 0)
-- Dependencies: 170
-- Name: COLUMN "CurrencyType"."ShortName"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "CurrencyType"."ShortName" IS 'РЎвЂљРЎР‚Р ВµРЎвЂ¦Р В±РЎС“Р С”Р Р†Р ВµР Р…Р Р…Р С•Р Вµ Р С•Р В±Р С•Р В·Р Р…Р В°РЎвЂЎР ВµР Р…Р С‘Р Вµ';


--
-- TOC entry 171 (class 1259 OID 6175256)
-- Name: MontlyReport; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "MontlyReport" (
    "Id" uuid NOT NULL,
    "StartBalance" money,
    "EndBalance" money,
    "StartDate" timestamp without time zone NOT NULL,
    "WalletId" uuid NOT NULL
);


ALTER TABLE public."MontlyReport" OWNER TO postgres;

--
-- TOC entry 2009 (class 0 OID 0)
-- Dependencies: 171
-- Name: TABLE "MontlyReport"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "MontlyReport" IS 'Р С—Р С•Р В»РЎРЉР В·Р С•Р Р†Р В°РЎвЂљР ВµР В»РЎРЉРЎРѓР С”Р С‘Р в„– Р С•РЎвЂљРЎвЂЎР ВµРЎвЂљ Р В·Р В° Р СР ВµРЎРѓРЎРЏРЎвЂ ';


--
-- TOC entry 2010 (class 0 OID 0)
-- Dependencies: 171
-- Name: COLUMN "MontlyReport"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."Id" IS 'Р С‘Р Т‘Р ВµР Р…РЎвЂљР С‘РЎвЂћР С‘Р С”Р В°РЎвЂљР С•РЎР‚';


--
-- TOC entry 2011 (class 0 OID 0)
-- Dependencies: 171
-- Name: COLUMN "MontlyReport"."StartBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."StartBalance" IS 'Р В±Р В°Р В»Р В°Р Р…РЎРѓ Р Р…Р В° Р Р…Р В°РЎвЂЎР В°Р В»Р С• Р СР ВµРЎРѓРЎРЏРЎвЂ Р В°';


--
-- TOC entry 2012 (class 0 OID 0)
-- Dependencies: 171
-- Name: COLUMN "MontlyReport"."EndBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."EndBalance" IS 'Р В±Р В°Р В»Р В°Р Р…РЎРѓ Р Р…Р В° Р С”Р С•Р Р…Р ВµРЎвЂ  Р СР ВµРЎРѓРЎРЏРЎвЂ Р В°';


--
-- TOC entry 2013 (class 0 OID 0)
-- Dependencies: 171
-- Name: COLUMN "MontlyReport"."StartDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."StartDate" IS 'Р СџР ВµРЎР‚Р С‘Р С•Р Т‘ Р Т‘Р ВµР в„–РЎРѓРЎвЂљР Р†Р С‘РЎРЏ (Р С–Р С•Р Т‘ Р СР ВµРЎРѓРЎРЏРЎвЂ )';


--
-- TOC entry 2014 (class 0 OID 0)
-- Dependencies: 171
-- Name: COLUMN "MontlyReport"."WalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "MontlyReport"."WalletId" IS 'Р ВР Т‘Р ВµР Р…РЎвЂљР С‘РЎвЂћР С‘Р С”Р В°РЎвЂљР С•РЎР‚ Р С”Р С•РЎв‚¬Р ВµР В»РЎРЉР С”Р В°';


--
-- TOC entry 172 (class 1259 OID 6175259)
-- Name: SystemUser; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "SystemUser" (
    "Id" uuid NOT NULL,
    "Name" character varying
);


ALTER TABLE public."SystemUser" OWNER TO postgres;

--
-- TOC entry 2015 (class 0 OID 0)
-- Dependencies: 172
-- Name: COLUMN "SystemUser"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "SystemUser"."Id" IS 'Р С‘Р Т‘Р ВµР Р…РЎвЂљР С‘РЎвЂћР С‘Р С”Р В°РЎвЂљР С•РЎР‚ Р С—Р С•Р В»РЎРЉР В·Р С•Р Р†Р В°РЎвЂљР ВµР В»РЎРЏ';


--
-- TOC entry 173 (class 1259 OID 6175265)
-- Name: Transaction; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
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


ALTER TABLE public."Transaction" OWNER TO postgres;

--
-- TOC entry 2016 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Id" IS 'Р С‘Р Т‘Р ВµР Р…РЎвЂљР С‘РЎвЂћР С‘Р С”Р В°РЎвЂљР С•РЎР‚';


--
-- TOC entry 2017 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."RegistrationDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."RegistrationDate" IS 'Р Т‘Р В°РЎвЂљР В° РЎР‚Р ВµР С–Р С‘РЎРѓРЎвЂљРЎР‚Р В°РЎвЂ Р С‘Р С‘ РЎвЂљРЎР‚Р В°Р Р…Р В·Р В°Р С”РЎвЂ Р С‘Р С‘ Р Р† РЎРѓР С‘РЎРѓРЎвЂљР ВµР СР Вµ';


--
-- TOC entry 2018 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."TransactionDate"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."TransactionDate" IS 'Р Т‘Р В°РЎвЂљР В° РЎРѓР С•Р Р†Р ВµРЎР‚РЎв‚¬Р ВµР Р…Р С‘РЎРЏ РЎвЂљРЎР‚Р В°Р Р…Р В·Р В°Р С”РЎвЂ Р С‘Р С‘';


--
-- TOC entry 2019 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."Code"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Code" IS 'Р С”Р С•Р Т‘ РЎвЂљРЎР‚Р В°Р Р…Р В·Р В°Р С”РЎвЂ Р С‘Р С‘';


--
-- TOC entry 2020 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."Description"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Description" IS 'Р С”Р С•Р СР СР ВµР Р…РЎвЂљР В°РЎР‚Р С‘Р в„–';


--
-- TOC entry 2021 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."CurrencyTypeId" IS 'Р Р†Р В°Р В»РЎР‹РЎвЂљР В° РЎвЂљРЎР‚Р В°Р Р…Р В·Р В°Р С”РЎвЂ Р С‘Р С‘';


--
-- TOC entry 2022 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."QuantityByTransaction"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."QuantityByTransaction" IS 'РЎРѓРЎС“Р СР СР В° Р Р† Р Р†Р В°Р В»РЎР‹РЎвЂљР Вµ РЎвЂљРЎР‚Р В°Р Р…Р В·Р В°Р С”РЎвЂ Р С‘Р С‘';


--
-- TOC entry 2023 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."QuantityByWallet"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."QuantityByWallet" IS 'РЎРѓРЎС“Р СР СР В° Р Р† Р Р†Р В°Р В»РЎР‹РЎвЂљР Вµ РЎРѓРЎвЂЎР ВµРЎвЂљР В°';


--
-- TOC entry 2024 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."Commission"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."Commission" IS 'Р С”Р С•Р СР С‘РЎРѓРЎРѓР С‘РЎРЏ';


--
-- TOC entry 2025 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."FromWalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."FromWalletId" IS 'Р С—Р ВµРЎР‚Р ВµР Р†Р С•Р Т‘ РЎРѓ РЎРѓРЎвЂЎР ВµРЎвЂљР В°';


--
-- TOC entry 2026 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "Transaction"."ToWalletId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Transaction"."ToWalletId" IS 'Р С—Р ВµРЎР‚Р ВµР Р†Р С•Р Т‘ Р Р…Р В° РЎРѓРЎвЂЎР ВµРЎвЂљ';


--
-- TOC entry 174 (class 1259 OID 6175271)
-- Name: Wallet; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Wallet" (
    "Id" uuid NOT NULL,
    "Name" character varying,
    "StartBalance" money,
    "Balance" money,
    "SystemUserId" uuid,
    "CurrencyTypeId" integer NOT NULL
);


ALTER TABLE public."Wallet" OWNER TO postgres;

--
-- TOC entry 2027 (class 0 OID 0)
-- Dependencies: 174
-- Name: TABLE "Wallet"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE "Wallet" IS 'Р С—Р С•Р В»РЎРЉР В·Р С•Р Р†Р В°РЎвЂљР ВµР В»РЎРЉРЎРѓР С”Р С‘Р Вµ РЎРѓРЎвЂЎР ВµРЎвЂљР В°';


--
-- TOC entry 2028 (class 0 OID 0)
-- Dependencies: 174
-- Name: COLUMN "Wallet"."Id"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."Id" IS 'Р С‘Р Т‘Р ВµР Р…РЎвЂљР С‘РЎвЂћР С‘Р С”Р В°РЎвЂљР С•РЎР‚';


--
-- TOC entry 2029 (class 0 OID 0)
-- Dependencies: 174
-- Name: COLUMN "Wallet"."StartBalance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."StartBalance" IS 'Р С—Р ВµРЎР‚Р Р†Р С•Р Р…Р В°РЎвЂЎР В°Р В»РЎРЉР Р…РЎвЂ№Р в„– Р В±Р В°Р В»Р В°Р Р…РЎРѓ РЎРѓРЎвЂЎР ВµРЎвЂљР В°';


--
-- TOC entry 2030 (class 0 OID 0)
-- Dependencies: 174
-- Name: COLUMN "Wallet"."Balance"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."Balance" IS 'Р С‘РЎвЂљР С•Р С–Р С•Р Р†Р С•Р Вµ РЎРѓР С•РЎРѓРЎвЂљР С•РЎРЏР Р…Р С‘Р Вµ РЎРѓРЎвЂЎР ВµРЎвЂљР В°';


--
-- TOC entry 2031 (class 0 OID 0)
-- Dependencies: 174
-- Name: COLUMN "Wallet"."SystemUserId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."SystemUserId" IS 'Р С—Р С•Р В»РЎРЉР В·Р С•Р Р†Р В°РЎвЂљР ВµР В»РЎРЉ';


--
-- TOC entry 2032 (class 0 OID 0)
-- Dependencies: 174
-- Name: COLUMN "Wallet"."CurrencyTypeId"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "Wallet"."CurrencyTypeId" IS 'Р Р†Р В°Р В»РЎР‹РЎвЂљР В° Р С”Р С•РЎв‚¬Р ВµР В»РЎРЉР С”Р В°';


--
-- TOC entry 175 (class 1259 OID 6175277)
-- Name: currency_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE currency_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.currency_id_seq OWNER TO postgres;

--
-- TOC entry 2033 (class 0 OID 0)
-- Dependencies: 175
-- Name: currency_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE currency_id_seq OWNED BY "CurrencyType"."Id";


--
-- TOC entry 1857 (class 2604 OID 6175361)
-- Name: Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Bank" ALTER COLUMN "Id" SET DEFAULT nextval('"Bank_Id_seq"'::regclass);


--
-- TOC entry 1854 (class 2604 OID 6175279)
-- Name: Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CurrencyType" ALTER COLUMN "Id" SET DEFAULT nextval('currency_id_seq'::regclass);


--
-- TOC entry 1871 (class 2606 OID 6175369)
-- Name: pk_bank_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Bank"
    ADD CONSTRAINT pk_bank_id PRIMARY KEY ("Id");


--
-- TOC entry 1869 (class 2606 OID 6175393)
-- Name: pk_coursearhive_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT pk_coursearhive_id PRIMARY KEY ("BankId", "RegDate");


--
-- TOC entry 1859 (class 2606 OID 6175283)
-- Name: pk_currency_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CurrencyType"
    ADD CONSTRAINT pk_currency_id PRIMARY KEY ("Id");


--
-- TOC entry 1861 (class 2606 OID 6175285)
-- Name: pk_montly_report_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "MontlyReport"
    ADD CONSTRAINT pk_montly_report_id PRIMARY KEY ("Id");


--
-- TOC entry 1865 (class 2606 OID 6175287)
-- Name: pk_transaction_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT pk_transaction_id PRIMARY KEY ("Id");


--
-- TOC entry 1867 (class 2606 OID 6175289)
-- Name: pk_user_account_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT pk_user_account_id PRIMARY KEY ("Id");


--
-- TOC entry 1863 (class 2606 OID 6175291)
-- Name: pk_user_id; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "SystemUser"
    ADD CONSTRAINT pk_user_id PRIMARY KEY ("Id");


--
-- TOC entry 1881 (class 2606 OID 6175387)
-- Name: fk_coursearhive_bank; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT fk_coursearhive_bank FOREIGN KEY ("BankId") REFERENCES "Bank"("Id");


--
-- TOC entry 1879 (class 2606 OID 6175377)
-- Name: fk_coursearhive_currencytype; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT fk_coursearhive_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 1880 (class 2606 OID 6175382)
-- Name: fk_coursearhive_currencytypebase; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "CourseArhive"
    ADD CONSTRAINT fk_coursearhive_currencytypebase FOREIGN KEY ("CurrencyTypeBaseId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 1873 (class 2606 OID 6175297)
-- Name: fk_transaction_currency; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transaction_currency FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 1874 (class 2606 OID 6175302)
-- Name: fk_transaction_montly_report; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transaction_montly_report FOREIGN KEY ("MontlyReportId") REFERENCES "MontlyReport"("Id");


--
-- TOC entry 1875 (class 2606 OID 6175307)
-- Name: fk_transactionfromwallet_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transactionfromwallet_wallet FOREIGN KEY ("FromWalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 1876 (class 2606 OID 6175312)
-- Name: fk_transactiontowallet_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Transaction"
    ADD CONSTRAINT fk_transactiontowallet_wallet FOREIGN KEY ("ToWalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 1877 (class 2606 OID 6175317)
-- Name: fk_wallet_currencytype; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT fk_wallet_currencytype FOREIGN KEY ("CurrencyTypeId") REFERENCES "CurrencyType"("Id");


--
-- TOC entry 1878 (class 2606 OID 6175322)
-- Name: fk_wallet_systemuser; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Wallet"
    ADD CONSTRAINT fk_wallet_systemuser FOREIGN KEY ("SystemUserId") REFERENCES "SystemUser"("Id");


--
-- TOC entry 1872 (class 2606 OID 6175327)
-- Name: pk_montlyreport_wallet; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "MontlyReport"
    ADD CONSTRAINT pk_montlyreport_wallet FOREIGN KEY ("WalletId") REFERENCES "Wallet"("Id");


--
-- TOC entry 1995 (class 0 OID 0)
-- Dependencies: 6
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2016-09-07 16:45:17

--
-- PostgreSQL database dump complete
--

