namespace FetPamily.Domain.Shared;

public static class Constants
{
    // BREED ENTITY
    public const int BREED_MAX_NAME_LENGTH = 200;

    // SPECIES ENTITY
    public const int SPECIES_MAX_NAME_LENGTH = 200;

    // PET ENTITY
    public const int PET_MAX_NAME_LENGTH = 100;
    public const int PET_MAX_DESCRIPTION_LENGTH = 2000;
    public const int PET_MAX_WEIGHT = 100;
    public const int PET_MAX_HEIGHT = 10000; // сантиметры

    // VOLUNTEER ENTITY
    public const int VOLUNTEER_MAX_FULLNAME_LENGTH = 150;
    public const int VOLUNTEER_MAX_EMAIL_LENGTH = 320;
    public const int VOLUNTEER_MAX_DESCRIPTION_LENGTH = 2000;

    // PET ADDRESS VO
    public const int ADDRESS_MAX_CITY_LENGTH = 100;
    public const int ADDRESS_MAX_STREET_LENGTH = 100;
    public const int ADDRESS_MAX_BUILDING_LENGTH = 10;

    // PAYMENT DETAIL VO (используется в VOLUNTEER и PET)
    public const int PAYMENT_MAX_NAME_LENGTH = 100;
    public const int PAYMENT_MAX_DESCRIPTION_LENGTH = 500;
    public const int PAYMENT_MAX_VALUE_LENGTH = 100;
}
