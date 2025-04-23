/// <reference path="../node_modules/@types/xrm/index.d.ts" />
declare namespace AccountEnum {
    const enum address1_addresstypecode {
        BillTo = 1,
        ShipTo = 2,
        Primary = 3,
        Other = 4,
    }

    const enum address2_freighttermscode {
        DefaultValue = 1,
    }

    const enum accountcategorycode {
        PreferredCustomer = 1,
        Standard = 2,
    }

    const enum paymenttermscode {
        Net30 = 1,
        _210Net30 = 2,
        Net45 = 3,
        Net60 = 4,
    }

    const enum accountratingcode {
        DefaultValue = 1,
    }

    const enum accountclassificationcode {
        DefaultValue = 1,
    }

    const enum preferredappointmenttimecode {
        Morning = 1,
        Afternoon = 2,
        Evening = 3,
    }

    const enum address2_shippingmethodcode {
        DefaultValue = 1,
    }

    const enum address2_addresstypecode {
        DefaultValue = 1,
    }

    const enum preferredappointmentdaycode {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
    }

    const enum businesstypecode {
        DefaultValue = 1,
    }

    const enum customertypecode {
        Competitor = 1,
        Consultant = 2,
        Customer = 3,
        Investor = 4,
        Partner = 5,
        Influencer = 6,
        Press = 7,
        Prospect = 8,
        Reseller = 9,
        Supplier = 10,
        Vendor = 11,
        Other = 12,
    }

    const enum shippingmethodcode {
        DefaultValue = 1,
    }

    const enum address1_freighttermscode {
        Fob = 1,
        NoCharge = 2,
    }

    const enum address1_shippingmethodcode {
        Airborne = 1,
        Dhl = 2,
        Fedex = 3,
        Ups = 4,
        PostalMail = 5,
        FullLoad = 6,
        WillCall = 7,
    }

    const enum industrycode {
        Accounting = 1,
        AgricultureAndNonPetrolNaturalResourceExtraction = 2,
        BroadcastingPrintingAndPublishing = 3,
        Brokers = 4,
        BuildingSupplyRetail = 5,
        BusinessServices = 6,
        Consulting = 7,
        ConsumerServices = 8,
        DesignDirectionAndCreativeManagement = 9,
        DistributorsDispatchersAndProcessors = 10,
        DoctorSOfficesAndClinics = 11,
        DurableManufacturing = 12,
        EatingAndDrinkingPlaces = 13,
        EntertainmentRetail = 14,
        EquipmentRentalAndLeasing = 15,
        Financial = 16,
        FoodAndTobaccoProcessing = 17,
        InboundCapitalIntensiveProcessing = 18,
        InboundRepairAndServices = 19,
        Insurance = 20,
        LegalServices = 21,
        NonDurableMerchandiseRetail = 22,
        OutboundConsumerService = 23,
        PetrochemicalExtractionAndDistribution = 24,
        ServiceRetail = 25,
        SigAffiliations = 26,
        SocialServices = 27,
        SpecialOutboundTradeContractors = 28,
        SpecialtyRealty = 29,
        Transportation = 30,
        UtilityCreationAndDistribution = 31,
        VehicleRetail = 32,
        Wholesale = 33,
    }

    const enum customersizecode {
        DefaultValue = 1,
    }

    const enum territorycode {
        DefaultValue = 1,
    }

    const enum ownershipcode {
        Public = 1,
        Private = 2,
        Subsidiary = 3,
        Other = 4,
    }

    const enum preferredcontactmethodcode {
        Any = 1,
        Email = 2,
        Phone = 3,
        Fax = 4,
        Mail = 5,
    }

}

declare namespace Xrm {
    type Account = Omit<FormContext, 'getAttribute'> & Omit<FormContext, 'getControl'> & AccountAttributes;

    interface EventContext {
        getFormContext(): Account;
    }

    interface AccountAttributes {
        getAttribute(name: "accountcategorycode"): Attributes.OptionSetAttribute;
        getAttribute(name: "accountclassificationcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "accountid"): Attributes.StringAttribute;
        getAttribute(name: "accountnumber"): Attributes.StringAttribute;
        getAttribute(name: "accountratingcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address1_addresstypecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address1_city"): Attributes.StringAttribute;
        getAttribute(name: "address1_composite"): Attributes.StringAttribute;
        getAttribute(name: "address1_country"): Attributes.StringAttribute;
        getAttribute(name: "address1_county"): Attributes.StringAttribute;
        getAttribute(name: "address1_fax"): Attributes.StringAttribute;
        getAttribute(name: "address1_freighttermscode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address1_latitude"): Attributes.NumberAttribute;
        getAttribute(name: "address1_line1"): Attributes.StringAttribute;
        getAttribute(name: "address1_line2"): Attributes.StringAttribute;
        getAttribute(name: "address1_line3"): Attributes.StringAttribute;
        getAttribute(name: "address1_longitude"): Attributes.NumberAttribute;
        getAttribute(name: "address1_name"): Attributes.StringAttribute;
        getAttribute(name: "address1_postalcode"): Attributes.StringAttribute;
        getAttribute(name: "address1_postofficebox"): Attributes.StringAttribute;
        getAttribute(name: "address1_primarycontactname"): Attributes.StringAttribute;
        getAttribute(name: "address1_shippingmethodcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address1_stateorprovince"): Attributes.StringAttribute;
        getAttribute(name: "address1_telephone1"): Attributes.StringAttribute;
        getAttribute(name: "address1_telephone2"): Attributes.StringAttribute;
        getAttribute(name: "address1_telephone3"): Attributes.StringAttribute;
        getAttribute(name: "address1_upszone"): Attributes.StringAttribute;
        getAttribute(name: "address1_utcoffset"): Attributes.NumberAttribute;
        getAttribute(name: "address2_addresstypecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address2_city"): Attributes.StringAttribute;
        getAttribute(name: "address2_composite"): Attributes.StringAttribute;
        getAttribute(name: "address2_country"): Attributes.StringAttribute;
        getAttribute(name: "address2_county"): Attributes.StringAttribute;
        getAttribute(name: "address2_fax"): Attributes.StringAttribute;
        getAttribute(name: "address2_freighttermscode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address2_latitude"): Attributes.NumberAttribute;
        getAttribute(name: "address2_line1"): Attributes.StringAttribute;
        getAttribute(name: "address2_line2"): Attributes.StringAttribute;
        getAttribute(name: "address2_line3"): Attributes.StringAttribute;
        getAttribute(name: "address2_longitude"): Attributes.NumberAttribute;
        getAttribute(name: "address2_name"): Attributes.StringAttribute;
        getAttribute(name: "address2_postalcode"): Attributes.StringAttribute;
        getAttribute(name: "address2_postofficebox"): Attributes.StringAttribute;
        getAttribute(name: "address2_primarycontactname"): Attributes.StringAttribute;
        getAttribute(name: "address2_shippingmethodcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "address2_stateorprovince"): Attributes.StringAttribute;
        getAttribute(name: "address2_telephone1"): Attributes.StringAttribute;
        getAttribute(name: "address2_telephone2"): Attributes.StringAttribute;
        getAttribute(name: "address2_telephone3"): Attributes.StringAttribute;
        getAttribute(name: "address2_upszone"): Attributes.StringAttribute;
        getAttribute(name: "address2_utcoffset"): Attributes.NumberAttribute;
        getAttribute(name: "aging30"): Attributes.NumberAttribute;
        getAttribute(name: "aging60"): Attributes.NumberAttribute;
        getAttribute(name: "aging90"): Attributes.NumberAttribute;
        getAttribute(name: "businesstypecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "createdby"): Attributes.LookupAttribute;
        getAttribute(name: "createdbyexternalparty"): Attributes.LookupAttribute;
        getAttribute(name: "createdon"): Attributes.DateAttribute;
        getAttribute(name: "createdonbehalfby"): Attributes.LookupAttribute;
        getAttribute(name: "creditlimit"): Attributes.NumberAttribute;
        getAttribute(name: "creditonhold"): Attributes.BooleanAttribute;
        getAttribute(name: "customersizecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "customertypecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "defaultpricelevelid"): Attributes.LookupAttribute;
        getAttribute(name: "description"): Attributes.StringAttribute;
        getAttribute(name: "donotbulkemail"): Attributes.BooleanAttribute;
        getAttribute(name: "donotbulkpostalmail"): Attributes.BooleanAttribute;
        getAttribute(name: "donotemail"): Attributes.BooleanAttribute;
        getAttribute(name: "donotfax"): Attributes.BooleanAttribute;
        getAttribute(name: "donotphone"): Attributes.BooleanAttribute;
        getAttribute(name: "donotpostalmail"): Attributes.BooleanAttribute;
        getAttribute(name: "donotsendmm"): Attributes.BooleanAttribute;
        getAttribute(name: "emailaddress1"): Attributes.StringAttribute;
        getAttribute(name: "emailaddress2"): Attributes.StringAttribute;
        getAttribute(name: "emailaddress3"): Attributes.StringAttribute;
        getAttribute(name: "exchangerate"): Attributes.NumberAttribute;
        getAttribute(name: "fax"): Attributes.StringAttribute;
        getAttribute(name: "followemail"): Attributes.BooleanAttribute;
        getAttribute(name: "ftpsiteurl"): Attributes.StringAttribute;
        getAttribute(name: "importsequencenumber"): Attributes.NumberAttribute;
        getAttribute(name: "industrycode"): Attributes.OptionSetAttribute;
        getAttribute(name: "lastonholdtime"): Attributes.DateAttribute;
        getAttribute(name: "lastusedincampaign"): Attributes.DateAttribute;
        getAttribute(name: "marketcap"): Attributes.NumberAttribute;
        getAttribute(name: "marketingonly"): Attributes.BooleanAttribute;
        getAttribute(name: "masterid"): Attributes.LookupAttribute;
        getAttribute(name: "merged"): Attributes.BooleanAttribute;
        getAttribute(name: "modifiedby"): Attributes.LookupAttribute;
        getAttribute(name: "modifiedbyexternalparty"): Attributes.LookupAttribute;
        getAttribute(name: "modifiedon"): Attributes.DateAttribute;
        getAttribute(name: "modifiedonbehalfby"): Attributes.LookupAttribute;
        getAttribute(name: "msdyn_salesaccelerationinsightidname"): Attributes.StringAttribute;
        getAttribute(name: "msdyn_segmentid"): Attributes.LookupAttribute;
        getAttribute(name: "msdyn_segmentidname"): Attributes.StringAttribute;
        getAttribute(name: "name"): Attributes.StringAttribute;
        getAttribute(name: "numberofemployees"): Attributes.NumberAttribute;
        getAttribute(name: "onholdtime"): Attributes.NumberAttribute;
        getAttribute(name: "opendeals"): Attributes.NumberAttribute;
        getAttribute(name: "opendeals_date"): Attributes.DateAttribute;
        getAttribute(name: "opendeals_state"): Attributes.NumberAttribute;
        getAttribute(name: "openrevenue"): Attributes.NumberAttribute;
        getAttribute(name: "openrevenue_date"): Attributes.DateAttribute;
        getAttribute(name: "openrevenue_state"): Attributes.NumberAttribute;
        getAttribute(name: "originatingleadid"): Attributes.LookupAttribute;
        getAttribute(name: "overriddencreatedon"): Attributes.DateAttribute;
        getAttribute(name: "ownerid"): Attributes.LookupAttribute;
        getAttribute(name: "ownershipcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "owningbusinessunit"): Attributes.LookupAttribute;
        getAttribute(name: "owningbusinessunitname"): Attributes.StringAttribute;
        getAttribute(name: "owningteam"): Attributes.LookupAttribute;
        getAttribute(name: "owninguser"): Attributes.LookupAttribute;
        getAttribute(name: "parentaccountid"): Attributes.LookupAttribute;
        getAttribute(name: "participatesinworkflow"): Attributes.BooleanAttribute;
        getAttribute(name: "paymenttermscode"): Attributes.OptionSetAttribute;
        getAttribute(name: "preferredappointmentdaycode"): Attributes.OptionSetAttribute;
        getAttribute(name: "preferredappointmenttimecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "preferredcontactmethodcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "preferredequipmentid"): Attributes.LookupAttribute;
        getAttribute(name: "preferredserviceid"): Attributes.LookupAttribute;
        getAttribute(name: "preferredsystemuserid"): Attributes.LookupAttribute;
        getAttribute(name: "primarycontactid"): Attributes.LookupAttribute;
        getAttribute(name: "primarysatoriid"): Attributes.StringAttribute;
        getAttribute(name: "primarytwitterid"): Attributes.StringAttribute;
        getAttribute(name: "revenue"): Attributes.NumberAttribute;
        getAttribute(name: "sharesoutstanding"): Attributes.NumberAttribute;
        getAttribute(name: "shippingmethodcode"): Attributes.OptionSetAttribute;
        getAttribute(name: "sic"): Attributes.StringAttribute;
        getAttribute(name: "slaid"): Attributes.LookupAttribute;
        getAttribute(name: "slainvokedid"): Attributes.LookupAttribute;
        getAttribute(name: "statecode"): Attributes.OptionSetAttribute;
        getAttribute(name: "statuscode"): Attributes.OptionSetAttribute;
        getAttribute(name: "stockexchange"): Attributes.StringAttribute;
        getAttribute(name: "telephone1"): Attributes.StringAttribute;
        getAttribute(name: "telephone2"): Attributes.StringAttribute;
        getAttribute(name: "telephone3"): Attributes.StringAttribute;
        getAttribute(name: "territorycode"): Attributes.OptionSetAttribute;
        getAttribute(name: "territoryid"): Attributes.LookupAttribute;
        getAttribute(name: "tickersymbol"): Attributes.StringAttribute;
        getAttribute(name: "timespentbymeonemailandmeetings"): Attributes.StringAttribute;
        getAttribute(name: "timezoneruleversionnumber"): Attributes.NumberAttribute;
        getAttribute(name: "transactioncurrencyid"): Attributes.LookupAttribute;
        getAttribute(name: "traversedpath"): Attributes.StringAttribute;
        getAttribute(name: "utcconversiontimezonecode"): Attributes.NumberAttribute;
        getAttribute(name: "websiteurl"): Attributes.StringAttribute;
        getAttribute(name: "yominame"): Attributes.StringAttribute;
        getControl(name: "accountcategorycode"): Controls.OptionSetControl;
        getControl(name: "accountclassificationcode"): Controls.OptionSetControl;
        getControl(name: "accountid"): Controls.StringControl;
        getControl(name: "accountnumber"): Controls.StringControl;
        getControl(name: "accountratingcode"): Controls.OptionSetControl;
        getControl(name: "address1_addresstypecode"): Controls.OptionSetControl;
        getControl(name: "address1_city"): Controls.StringControl;
        getControl(name: "address1_composite"): Controls.StringControl;
        getControl(name: "address1_country"): Controls.StringControl;
        getControl(name: "address1_county"): Controls.StringControl;
        getControl(name: "address1_fax"): Controls.StringControl;
        getControl(name: "address1_freighttermscode"): Controls.OptionSetControl;
        getControl(name: "address1_latitude"): Controls.NumberControl;
        getControl(name: "address1_line1"): Controls.StringControl;
        getControl(name: "address1_line2"): Controls.StringControl;
        getControl(name: "address1_line3"): Controls.StringControl;
        getControl(name: "address1_longitude"): Controls.NumberControl;
        getControl(name: "address1_name"): Controls.StringControl;
        getControl(name: "address1_postalcode"): Controls.StringControl;
        getControl(name: "address1_postofficebox"): Controls.StringControl;
        getControl(name: "address1_primarycontactname"): Controls.StringControl;
        getControl(name: "address1_shippingmethodcode"): Controls.OptionSetControl;
        getControl(name: "address1_stateorprovince"): Controls.StringControl;
        getControl(name: "address1_telephone1"): Controls.StringControl;
        getControl(name: "address1_telephone2"): Controls.StringControl;
        getControl(name: "address1_telephone3"): Controls.StringControl;
        getControl(name: "address1_upszone"): Controls.StringControl;
        getControl(name: "address1_utcoffset"): Controls.NumberControl;
        getControl(name: "address2_addresstypecode"): Controls.OptionSetControl;
        getControl(name: "address2_city"): Controls.StringControl;
        getControl(name: "address2_composite"): Controls.StringControl;
        getControl(name: "address2_country"): Controls.StringControl;
        getControl(name: "address2_county"): Controls.StringControl;
        getControl(name: "address2_fax"): Controls.StringControl;
        getControl(name: "address2_freighttermscode"): Controls.OptionSetControl;
        getControl(name: "address2_latitude"): Controls.NumberControl;
        getControl(name: "address2_line1"): Controls.StringControl;
        getControl(name: "address2_line2"): Controls.StringControl;
        getControl(name: "address2_line3"): Controls.StringControl;
        getControl(name: "address2_longitude"): Controls.NumberControl;
        getControl(name: "address2_name"): Controls.StringControl;
        getControl(name: "address2_postalcode"): Controls.StringControl;
        getControl(name: "address2_postofficebox"): Controls.StringControl;
        getControl(name: "address2_primarycontactname"): Controls.StringControl;
        getControl(name: "address2_shippingmethodcode"): Controls.OptionSetControl;
        getControl(name: "address2_stateorprovince"): Controls.StringControl;
        getControl(name: "address2_telephone1"): Controls.StringControl;
        getControl(name: "address2_telephone2"): Controls.StringControl;
        getControl(name: "address2_telephone3"): Controls.StringControl;
        getControl(name: "address2_upszone"): Controls.StringControl;
        getControl(name: "address2_utcoffset"): Controls.NumberControl;
        getControl(name: "aging30"): Controls.NumberControl;
        getControl(name: "aging60"): Controls.NumberControl;
        getControl(name: "aging90"): Controls.NumberControl;
        getControl(name: "businesstypecode"): Controls.OptionSetControl;
        getControl(name: "createdby"): Controls.LookupControl;
        getControl(name: "createdbyexternalparty"): Controls.LookupControl;
        getControl(name: "createdon"): Controls.DateControl;
        getControl(name: "createdonbehalfby"): Controls.LookupControl;
        getControl(name: "creditlimit"): Controls.NumberControl;
        getControl(name: "creditonhold"): Controls.StandardControl;
        getControl(name: "customersizecode"): Controls.OptionSetControl;
        getControl(name: "customertypecode"): Controls.OptionSetControl;
        getControl(name: "defaultpricelevelid"): Controls.LookupControl;
        getControl(name: "description"): Controls.StringControl;
        getControl(name: "donotbulkemail"): Controls.StandardControl;
        getControl(name: "donotbulkpostalmail"): Controls.StandardControl;
        getControl(name: "donotemail"): Controls.StandardControl;
        getControl(name: "donotfax"): Controls.StandardControl;
        getControl(name: "donotphone"): Controls.StandardControl;
        getControl(name: "donotpostalmail"): Controls.StandardControl;
        getControl(name: "donotsendmm"): Controls.StandardControl;
        getControl(name: "emailaddress1"): Controls.StringControl;
        getControl(name: "emailaddress2"): Controls.StringControl;
        getControl(name: "emailaddress3"): Controls.StringControl;
        getControl(name: "exchangerate"): Controls.NumberControl;
        getControl(name: "fax"): Controls.StringControl;
        getControl(name: "followemail"): Controls.StandardControl;
        getControl(name: "ftpsiteurl"): Controls.StringControl;
        getControl(name: "importsequencenumber"): Controls.NumberControl;
        getControl(name: "industrycode"): Controls.OptionSetControl;
        getControl(name: "lastonholdtime"): Controls.DateControl;
        getControl(name: "lastusedincampaign"): Controls.DateControl;
        getControl(name: "marketcap"): Controls.NumberControl;
        getControl(name: "marketingonly"): Controls.StandardControl;
        getControl(name: "masterid"): Controls.LookupControl;
        getControl(name: "merged"): Controls.StandardControl;
        getControl(name: "modifiedby"): Controls.LookupControl;
        getControl(name: "modifiedbyexternalparty"): Controls.LookupControl;
        getControl(name: "modifiedon"): Controls.DateControl;
        getControl(name: "modifiedonbehalfby"): Controls.LookupControl;
        getControl(name: "msdyn_salesaccelerationinsightidname"): Controls.StringControl;
        getControl(name: "msdyn_segmentid"): Controls.LookupControl;
        getControl(name: "msdyn_segmentidname"): Controls.StringControl;
        getControl(name: "name"): Controls.StringControl;
        getControl(name: "numberofemployees"): Controls.NumberControl;
        getControl(name: "onholdtime"): Controls.NumberControl;
        getControl(name: "opendeals"): Controls.NumberControl;
        getControl(name: "opendeals_date"): Controls.DateControl;
        getControl(name: "opendeals_state"): Controls.NumberControl;
        getControl(name: "openrevenue"): Controls.NumberControl;
        getControl(name: "openrevenue_date"): Controls.DateControl;
        getControl(name: "openrevenue_state"): Controls.NumberControl;
        getControl(name: "originatingleadid"): Controls.LookupControl;
        getControl(name: "overriddencreatedon"): Controls.DateControl;
        getControl(name: "ownerid"): Controls.LookupControl;
        getControl(name: "ownershipcode"): Controls.OptionSetControl;
        getControl(name: "owningbusinessunit"): Controls.LookupControl;
        getControl(name: "owningbusinessunitname"): Controls.StringControl;
        getControl(name: "owningteam"): Controls.LookupControl;
        getControl(name: "owninguser"): Controls.LookupControl;
        getControl(name: "parentaccountid"): Controls.LookupControl;
        getControl(name: "participatesinworkflow"): Controls.StandardControl;
        getControl(name: "paymenttermscode"): Controls.OptionSetControl;
        getControl(name: "preferredappointmentdaycode"): Controls.OptionSetControl;
        getControl(name: "preferredappointmenttimecode"): Controls.OptionSetControl;
        getControl(name: "preferredcontactmethodcode"): Controls.OptionSetControl;
        getControl(name: "preferredequipmentid"): Controls.LookupControl;
        getControl(name: "preferredserviceid"): Controls.LookupControl;
        getControl(name: "preferredsystemuserid"): Controls.LookupControl;
        getControl(name: "primarycontactid"): Controls.LookupControl;
        getControl(name: "primarysatoriid"): Controls.StringControl;
        getControl(name: "primarytwitterid"): Controls.StringControl;
        getControl(name: "revenue"): Controls.NumberControl;
        getControl(name: "sharesoutstanding"): Controls.NumberControl;
        getControl(name: "shippingmethodcode"): Controls.OptionSetControl;
        getControl(name: "sic"): Controls.StringControl;
        getControl(name: "slaid"): Controls.LookupControl;
        getControl(name: "slainvokedid"): Controls.LookupControl;
        getControl(name: "statecode"): Controls.OptionSetControl;
        getControl(name: "statuscode"): Controls.OptionSetControl;
        getControl(name: "stockexchange"): Controls.StringControl;
        getControl(name: "telephone1"): Controls.StringControl;
        getControl(name: "telephone2"): Controls.StringControl;
        getControl(name: "telephone3"): Controls.StringControl;
        getControl(name: "territorycode"): Controls.OptionSetControl;
        getControl(name: "territoryid"): Controls.LookupControl;
        getControl(name: "tickersymbol"): Controls.StringControl;
        getControl(name: "timespentbymeonemailandmeetings"): Controls.StringControl;
        getControl(name: "timezoneruleversionnumber"): Controls.NumberControl;
        getControl(name: "transactioncurrencyid"): Controls.LookupControl;
        getControl(name: "traversedpath"): Controls.StringControl;
        getControl(name: "utcconversiontimezonecode"): Controls.NumberControl;
        getControl(name: "websiteurl"): Controls.StringControl;
        getControl(name: "yominame"): Controls.StringControl;
    }

}

