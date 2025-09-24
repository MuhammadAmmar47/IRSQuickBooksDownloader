Namespace Orders
    Public Enum RejectCodeType
        None = 0
        Address = 9
        Unprocessable = 10
        Social_Security_Number = 3
        Illegible = 1
        Name_does_not_match_record = 11
        Old_Signature_date = 4

        Altered = 2
        Invalid_signature = 5
        Incomplete_form = 6
        Invalid_form_request = 12
        Missing_line_5 = 13
        Did_not_file_jointly = 14
        Invalid_product_request = 15

        MISSING_4506 = 90
        Name_differs_on_coversheet = 91
        Years_differ_on_4506T = 92

        T4506_Request_spouse = 93
        Duplicate_Name_on_coversheet = 94
        Batch_Processed_within_last_3_days = 95

        Request_on_4506_differ = 96
        Page_cutoff_during_transmission = 97
        Reference_number_missing = 98
    End Enum
End Namespace