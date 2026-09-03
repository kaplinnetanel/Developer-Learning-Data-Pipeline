import os
import json
import pandas as pd

def create_directory(path: str):

    os.makedirs(path, exist_ok=True)

def load_data(file_path: str) -> pd.DataFrame:
    return pd.read_csv(file_path)

def clean_data(df: pd.DataFrame) -> pd.DataFrame:
    print(f"Duplicates found: {df.duplicated().sum()}")
    df = df.drop_duplicates()
    df['YearsCode'] = df['YearsCode'].astype('Int64')
    return df

def split_multiselect(value):
    if pd.isna(value):
        return None
    return value.split(";")

def process_multiselect_columns(df: pd.DataFrame) -> pd.DataFrame:
    df['LearnCode'] = df['LearnCode'].apply(split_multiselect)
    df['AILearnHow'] = df['AILearnHow'].apply(split_multiselect)
    return df

def save_to_jsonl(df: pd.DataFrame, file_path: str):
    with open(file_path, "w") as f:
        for _, row in df.iterrows():
            record = row.to_dict()
            if 'YearsCode' in record and pd.notna(record['YearsCode']):
                record['YearsCode'] = int(record['YearsCode'])
            elif 'YearsCode' in record:
                record['YearsCode'] = None
                
            for key, value in record.items():
                if not isinstance(value, list) and pd.isna(value):
                    record[key] = None
            f.write(json.dumps(record) + "\n")

def load_from_jsonl(file_path: str) -> pd.DataFrame:
    records = []
    with open(file_path, "r") as f:
        for line in f:
            records.append(json.loads(line))
    return pd.DataFrame(records)

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

def add_derived_columns(df: pd.DataFrame) -> pd.DataFrame:
    TECH_DOC = "Technical documentation"
    AI_CODEGEN = "Code generation AI"
    STACK_OVERFLOW = "Stack Overflow"

    df['experienceLevel'] = df['YearsCode'].apply(experience_level)
    df['usesDocumentation'] = df['LearnCode'].apply(lambda x: has_option(x, TECH_DOC))
    df['usesAIForLearning'] = df['LearnCode'].apply(lambda x: has_option(x, AI_CODEGEN))
    df['usesStackOverflow'] = df['LearnCode'].apply(lambda x: has_option(x, STACK_OVERFLOW))
    return df

def rename_columns(df: pd.DataFrame) -> pd.DataFrame:
    return df.rename(columns={
        'ResponseId': 'responseId',
        'Age': 'age',
        'YearsCode': 'yearsCode',
        'DevType': 'devType',
        'LearnCodeChoose': 'learnCodeChoose',
        'LearnCode': 'learnCode',
        'LearnCodeAI': 'learnCodeAI',
        'AILearnHow': 'aiLearningMethods',
        'AISelect': 'aiUsage',
        'AIAcc': 'aiTrust',
        'AISent': 'aiSentiment',
    })

def main():
    # הגדרת נתיבים
    work_dir = "../my_work"
    raw_data_path = "../data/developer_ai_learning_raw.csv"
    intermediate_jsonl = os.path.join(work_dir, "cleaned_data.jsonl")
    final_jsonl = os.path.join(work_dir, "processed_data.jsonl")

    # שלבי הריצה
    create_directory(work_dir)
    df = load_data(raw_data_path)
    df = clean_data(df)
    df = process_multiselect_columns(df)
    
    # שמירה וטעינה ביניים כפי שהופיע במחברת
    save_to_jsonl(df, intermediate_jsonl)
    df = load_from_jsonl(intermediate_jsonl)
    
    # הוספת עמודות ושינוי שמות
    df = add_derived_columns(df)
    df = rename_columns(df)
    
    # שמירה סופית
    save_to_jsonl(df, final_jsonl)
    print("Processing complete. Processed data saved successfully.")

if __name__ == "__main__":
    main()