import pandas as pd
def split_multiselect(value):
    if value is None or value == "":
        return None

    return value.split(";")

def experience_level(years_code):
    if pd.isna(years_code):
        return "Unknown"
    if years_code <= 2:
        return "Beginner"

    if years_code <= 5:
        return "Early Career"

    if years_code <= 10:
        return "Experienced"

    return "Highly Experienced"


def has_option(options, target):
    if options is None:
        return False

    return target in options

def clean_years_code(value):
    if value is None or value == "":
        return None

    return int(float(value))

def process_record(raw_record):
    record = raw_record.copy()

    record["YearsCode"] = clean_years_code(record["YearsCode"])
   
    record["LearnCode"] = split_multiselect(record["LearnCode"])
    record["AILearnHow"] = split_multiselect(record["AILearnHow"])

  
    record["experienceLevel"] = experience_level(
        record["YearsCode"]
    )

    record["usesDocumentation"] = has_option(
        record["LearnCode"],
        "Technical documentation (is generated for/by the tool or system)"
    )

    record["usesAIForLearning"] = has_option(
        record["LearnCode"],
        "AI CodeGen tools or AI-enabled apps"
    )

    record["usesStackOverflow"] = has_option(
        record["LearnCode"],
        "Stack Overflow or Stack Exchange"
    )

    return record

if __name__ == "__main__":
    test_record = {
        "YearsCode": 5,
        "LearnCode": "AI CodeGen tools or AI-enabled apps;Stack Overflow or Stack Exchange",
        "AILearnHow": "AI CodeGen tools or AI-enabled apps"
    }

    result = process_record(test_record)

    print(result)